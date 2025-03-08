package com.example.kinopoisk_10;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;
import androidx.fragment.app.DialogFragment;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.google.android.gms.tasks.Continuation;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.android.youtube.player.YouTubeBaseActivity;
import com.google.android.youtube.player.YouTubeInitializationResult;
import com.google.android.youtube.player.YouTubePlayer;
import com.google.android.youtube.player.YouTubePlayerView;
import com.google.firebase.FirebaseApp;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.text.DecimalFormat;
import java.util.ArrayList;

import io.realm.Realm;

public class FilmInfoActivity extends AppCompatActivity implements FilmEditingDialogFragment.FilmEditingNoticeDialogListener {

    private Toolbar toolbar;
    private FilmEditingDialogFragment filmEditingDialogFragment;

    private FilmsVersion filmsVersion;
    private Film currentFilm;

    private FirebaseDatabase mFirebaseDatabase;
    private DatabaseReference mDatabaseReference;

    private StorageReference mStorageReference;

    private Realm mRealm;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_film_info_constraint);

        toolbar = findViewById(R.id.film_info_toolbar);
        setSupportActionBar(toolbar);

        Bundle extras = getIntent().getExtras();
        if (extras == null)
            return;

        initFirebase();

        mStorageReference = FirebaseStorage.getInstance().getReference();

        mRealm = Realm.getInstance(getApplicationContext());

        DecimalFormat decimalFormat = new DecimalFormat("#.##");

        ImageView filmImage;
        TextView textViewAboutFilm;

        currentFilm = getIntent().getParcelableExtra(Film.class.getCanonicalName());
        filmsVersion = new FilmsVersion(getIntent().getLongExtra("FilmsVersion", 0));

        filmImage = findViewById(R.id.film_info_image);
        Glide.with(this.getApplicationContext()).load(AllConstants.POSTER_URI_START + currentFilm.getPosterURI()).into(filmImage);

        textViewAboutFilm = findViewById(R.id.film_info_name);
        textViewAboutFilm.setText(currentFilm.getName());

        setTitle(currentFilm.getName());

        textViewAboutFilm = findViewById(R.id.film_info_about_tag_line);
        textViewAboutFilm.setText(currentFilm.getTagLine());

        textViewAboutFilm = findViewById(R.id.film_info_about_release_year);
        textViewAboutFilm.setText(String.valueOf(currentFilm.getReleaseYear()));

        textViewAboutFilm = findViewById(R.id.film_info_about_director);
        textViewAboutFilm.setText(currentFilm.getDirector());

        textViewAboutFilm = findViewById(R.id.film_info_about_genres);
        textViewAboutFilm.setText(genresToString(currentFilm.getGenres()));

        textViewAboutFilm = findViewById(R.id.film_info_about_duration);
        textViewAboutFilm.setText(String.valueOf(currentFilm.getDuration()));

        long ratingSum = currentFilm.getRatingsSum();
        long views = currentFilm.getViews();
        double filmRating = (double)ratingSum / views;
        textViewAboutFilm = findViewById(R.id.film_info_about_rating);
        textViewAboutFilm.setText(decimalFormat.format(filmRating));

        textViewAboutFilm = findViewById(R.id.film_info_about_views);
        textViewAboutFilm.setText(String.valueOf(views));

        textViewAboutFilm = findViewById(R.id.film_info_text_view_description);
        textViewAboutFilm.setText(currentFilm.getDescription());

        final String videoURI = currentFilm.getTrailerURI();

        checkPlayButton(videoURI);

    }

    @Override
    public void onBackPressed() {
        // call the superclass method first
        super.onStop();
        Intent intent = new Intent();
        intent.putExtra(Film.class.getCanonicalName(), currentFilm);
        intent.putExtra("FilmsVersion", filmsVersion.getVersion());
        setResult(RESULT_OK, intent);
        finish();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.film_editing_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case R.id.action_edit_film:
                showFilmEditingDialog();
                break;
        }

        return true;
    }

    public void showFilmEditingDialog() {

        if (filmEditingDialogFragment == null)
            filmEditingDialogFragment = new FilmEditingDialogFragment();

        filmEditingDialogFragment.setCurrentFilmTrailerURI(currentFilm.getTrailerURI());
        filmEditingDialogFragment.show(getSupportFragmentManager(), "settings");
    }

    @Override
    public void onFilmEditingDialogPositiveClick(DialogFragment dialog) {
        FilmEditingDialogFragment myDialogFragment = (FilmEditingDialogFragment) dialog;

        final String[] trailerURI = {myDialogFragment.getCurrentFilmTrailerURI()};
        String oldURI = currentFilm.getTrailerURI();

        if (myDialogFragment.getIsTrailerLocalFile()) {
            if (Build.VERSION.SDK_INT >= 23) {
                int permissionCheck = ContextCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.READ_EXTERNAL_STORAGE);
                if (permissionCheck != PackageManager.PERMISSION_GRANTED) {
                    ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.READ_EXTERNAL_STORAGE}, 1);
                }
            }

            String trailerPath = Environment.getExternalStorageDirectory().getPath() + "/" +
                    AllConstants.DEFAULT_TRAILERS_LOCAL_FOLDER_NAME + "/" +
                    myDialogFragment.getCurrentFilmTrailerLocalFile();

            File trailerFile = new File(trailerPath);

            if (!trailerFile.exists()) {
                Toast toast = Toast.makeText(getApplicationContext(),
                        getString(R.string.main_activity_film_adding_toast_no_such_file),
                        Toast.LENGTH_SHORT);
                toast.show();
                return;
            }

            Uri file = Uri.fromFile(trailerFile);
            final StorageReference trailerRef = mStorageReference.child(AllConstants.DEFAULT_FIREBASE_TRAILERS_FOLDER_NAME + "/" + file.getLastPathSegment());

            final UploadTask uploadTask = trailerRef.putFile(file);

            Toast toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.film_info_activity_film_editing_start_uploading_trailer),
                    Toast.LENGTH_SHORT);
            toast.show();

            // Register observers to listen for when the download is done or if it fails
            uploadTask.addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception exception) {
                    // Handle unsuccessful uploads
                    Toast toast = Toast.makeText(getApplicationContext(),
                            getString(R.string.film_info_activity_film_editing_failure_uploaded_trailer),
                            Toast.LENGTH_SHORT);
                    toast.show();
                }
            }).addOnSuccessListener(new OnSuccessListener<UploadTask.TaskSnapshot>() {
                @Override
                public void onSuccess(UploadTask.TaskSnapshot taskSnapshot) {
                    // taskSnapshot.getMetadata() contains file metadata such as size, content-type, etc.
                    // ...
                    trailerURI[0] = taskSnapshot.getUploadSessionUri().toString();
                    Toast toast = Toast.makeText(getApplicationContext(),
                            getString(R.string.film_info_activity_film_editing_success_uploaded_trailer),
                            Toast.LENGTH_SHORT);
                    toast.show();
                    Task<Uri> urlTask = uploadTask.continueWithTask(new Continuation<UploadTask.TaskSnapshot, Task<Uri>>() {
                        @Override
                        public Task<Uri> then(@NonNull Task<UploadTask.TaskSnapshot> task) throws Exception {
                            if (!task.isSuccessful()) {
                                throw task.getException();
                            }

                            // Continue with the task to get the download URL
                            return trailerRef.getDownloadUrl();
                        }
                    }).addOnCompleteListener(new OnCompleteListener<Uri>() {
                        @Override
                        public void onComplete(@NonNull Task<Uri> task) {
                            if (task.isSuccessful()) {
                                trailerURI[0] = task.getResult().toString();

                                currentFilm.setTrailerURI(trailerURI[0]);

                                checkPlayButton(currentFilm.getTrailerURI());
                                updateFilm();
                            } else {

                                // Handle failures
                                // ...
                            }
                        }
                    });
                }
            });
        }
        else {
            if (trailerURI[0] != null && !trailerURI[0].isEmpty())
                currentFilm.setTrailerURI(trailerURI[0]);
            else
                currentFilm.setTrailerURI("");

            checkPlayButton(currentFilm.getTrailerURI());

            if ((trailerURI[0] != null && !trailerURI[0].equals(oldURI)) || (oldURI != null && !oldURI.equals(trailerURI[0]))) {
                updateFilm();
            }
        }

    }

    @Override
    public void onFilmEditingDialogNegativeClick(DialogFragment dialog) {

    }

    private void checkPlayButton(final String videoURI) {
        Button playButton = findViewById(R.id.film_info_play_button);
        TextView videoInfo = findViewById(R.id.film_info_text_view_no_video);

        if (videoURI != null && !videoURI.isEmpty()) {
            playButton = findViewById(R.id.film_info_play_button);
            playButton.setVisibility(View.VISIBLE);

            playButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intent = new Intent(FilmInfoActivity.this, VideoActivity.class);
                    intent.putExtra("TrailerURI", videoURI);
                    startActivity(intent);
                }
            });

            videoInfo = findViewById(R.id.film_info_text_view_no_video);
            videoInfo.setVisibility(View.GONE);
        }
        else
        {
            playButton.setVisibility(View.GONE);
            videoInfo.setVisibility(View.VISIBLE);
        }
    }

    private String genresToString(ArrayList<String> genres) {
        String result = "";
        for (int i = 0; i < genres.size(); i++) {
            result += genres.get(i);
            if (i != genres.size() - 1)
                result += ", ";
        }
        return result;
    }

    private void updateFilm() {
        filmsVersion.setVersion(filmsVersion.getVersion() + 1);

        FirebaseHelper.setVersion(mDatabaseReference, filmsVersion.getVersion());
        RealmHelper.updateFilmsVersion(mRealm, filmsVersion);

        FirebaseHelper.updateFilm(mDatabaseReference, currentFilm);
        RealmHelper.updateFilm(mRealm, currentFilm);

        Toast toast = Toast.makeText(getApplicationContext(),
                getString(R.string.film_info_activity_film_editing_toast_success),
                Toast.LENGTH_SHORT);
        toast.show();
    }

    //////////////////////// Firebase ///////////////////////////

    private void initFirebase() {
        //инициализируем наше приложение для Firebase согласно параметрам в google-services.json
        // (google-services.json - файл, с настройками для firebase, кот. мы получили во время регистрации)
        FirebaseApp.initializeApp(this);
        //получаем точку входа для базы данных
        mFirebaseDatabase = FirebaseDatabase.getInstance();
        //получаем ссылку для работы с базой данных
        mDatabaseReference = mFirebaseDatabase.getReference();
    }

    /////////////////////////////////////////////////////////////

}

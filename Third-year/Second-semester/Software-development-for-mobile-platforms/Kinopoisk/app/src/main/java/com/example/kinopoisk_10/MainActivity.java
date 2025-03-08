package com.example.kinopoisk_10;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;
import androidx.fragment.app.DialogFragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.graphics.Typeface;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;

import com.google.android.gms.tasks.Continuation;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.FirebaseApp;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Locale;
import java.util.stream.Collectors;

import io.realm.Realm;
import io.realm.RealmConfiguration;
import io.realm.exceptions.RealmMigrationNeededException;

public class MainActivity extends AppCompatActivity implements SettingsDialogFragment.SettingsNoticeDialogListener,
                                                               FilterDialogFragment.FilterNoticeDialogListener,
                                                               FilmAddingDialogFragment.FilmAddingNoticeDialogListener {

    private static final long CURR_VERSION = 0;

    public static final String APP_IMAGES_PATH =
            Environment.getExternalStorageDirectory().getAbsolutePath() + "/Kinopoisk/" + "/Images/";


    private static final float TEXT_SIZE_START_VALUE = 0.75f; //начальное значение размера шрифта
    private static final float TEXT_SIZE_STEP = 0.05f; //шаг увеличения коэффициента
    private static final int DEFAULT_TEXT_SIZE_SCALE = 4; //начальный масштаб размера шрифта
    private static final String DEFAULT_FONT = "SERIF"; //начальный шрифт

    private static final int FILM_INFO_ACTIVITY_ID = 1;

    private Toolbar toolbar;
    private RecyclerView rvMain;
    private FilmAdapter filmAdapter;

    private SettingsDialogFragment settingsDialogFragment;
    private FilterDialogFragment filterDialogFragment;
    private FilmAddingDialogFragment filmAddingDialogFragment;

    private FilmsVersion filmsVersion;
    private ArrayList<Film> films;
    private ArrayList<Film> currentFilms;

    private FirebaseDatabase mFirebaseDatabase;
    private DatabaseReference mDatabaseReference;

    private StorageReference mStorageReference;

    private Realm mRealm;

    private int currentTextSizeScale;
    private String currentFont;
    private String currentLocale;

    private long currentFilterViewsStartRange;
    private long currentFilterViewsEndRange;
    private boolean wasAllFilmsRestart;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        //setTheme(R.style.AppTheme);

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        initFirebase();

        mStorageReference = FirebaseStorage.getInstance().getReference();

        mRealm = Realm.getInstance(getApplicationContext());

        ////////////////// Needed in case of reedit RealmFilm: uncomment and comment below 1 line once //////////////////

        /*
        RealmConfiguration configuration = new RealmConfiguration.Builder(getApplication()).deleteRealmIfMigrationNeeded().build();
        try {
            Realm.setDefaultConfiguration(configuration);
            mRealm = Realm.getDefaultInstance();
        } catch (RealmMigrationNeededException e) {
            Realm.deleteRealm(configuration);
            mRealm = Realm.getDefaultInstance();
        }
        */

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        currentTextSizeScale = DEFAULT_TEXT_SIZE_SCALE;
        currentFont = "fonts/Roboto_regular.ttf";
        currentLocale = "ru";

        currentFilterViewsStartRange = 100;
        currentFilterViewsEndRange = 500000;
        wasAllFilmsRestart = true;

        settingsDialogFragment = new SettingsDialogFragment(currentTextSizeScale, "Default", currentLocale);
        filterDialogFragment = new FilterDialogFragment();

        //Tool
        toolbar = findViewById(R.id.main_activity_toolbar);
        setSupportActionBar(toolbar);

        //Привяжем его к элементу
        rvMain = findViewById(R.id.rvRecyclerView);

        ///////////////////////////////////////////////////////////////////////////////

        //Remove all films and filmsVersions in Realm
        //
        //RealmHelper.removeAllFilms(mRealm);
        //RealmHelper.removeAllFilmsVersions(mRealm);

        ////// Uncomment and start program only once for saving data in all DB //////
/*
        filmsVersion = new FilmsVersion(CURR_VERSION);
        RealmHelper.addFilmsVersion(mRealm, filmsVersion);

        films = getFilms();
        for (Film film : films)
            RealmHelper.addFilm(mRealm, film);

        films = getFilms();
        for (Film film : films)
            FirebaseHelper.addFilm(mDatabaseReference, film);
*/
        //////////////////////////////////////////////////////////////////////////////

        ///////////////////// Uncomment if MainActivity is a start activity /////////////////////
/*
        films = new ArrayList<Film>();

        filmsVersion = RealmHelper.getFilmsVersion(mRealm);

        final long[] firebaseVersion = {-1};

        mDatabaseReference.child("version")
                .addListenerForSingleValueEvent(new ValueEventListener() {
                    //если данные в БД меняются
                    @Override
                    public void onDataChange(DataSnapshot dataSnapshot) {
                        firebaseVersion[0] = dataSnapshot.getValue(Long.class);

                        if (firebaseVersion[0] != filmsVersion.getVersion()) {
                            filmsVersion.setVersion(firebaseVersion[0]);
                            RealmHelper.updateFilmsVersion(mRealm, filmsVersion);
                            mDatabaseReference.child("films")
                                    .addListenerForSingleValueEvent(new ValueEventListener() {
                                        //если данные в БД меняются
                                        @Override
                                        public void onDataChange(DataSnapshot dataSnapshot) {
                                            //проходим по всем записям и помещаем их в list_users в виде класса User
                                            for (DataSnapshot postSnapshot : dataSnapshot.getChildren()) {
                                                Film film = postSnapshot.getValue(Film.class);
                                                films.add(film);
                                            }
                                            currentFilms = new ArrayList<>(films);
                                            redrawAdapter(films);
                                        }

                                        @Override
                                        public void onCancelled(DatabaseError databaseError) {

                                        }
                                    });
                        }
                        else {
                            //films = getFilms();
                            films = RealmHelper.getAllFilms(mRealm);
                            currentFilms = new ArrayList<>(films);
                            redrawAdapter(films);
                        }
                    }

                    @Override
                    public void onCancelled(DatabaseError databaseError) {

                    }
                });

*/

        ////////////////////////////////////////////////////////////////////////////////////////

        //films = getFilms();

        Bundle extras = getIntent().getExtras();
        if (extras == null)
            return;

        filmsVersion = new FilmsVersion(getIntent().getLongExtra("FilmsVersion", CURR_VERSION));

        films = getIntent().getParcelableArrayListExtra("AllFilms");

        if (films == null)
            films = new ArrayList<>();

        currentFilms = new ArrayList<>(films);
        //Создадим адаптер
        filmAdapter = new FilmAdapter(getApplicationContext(), films);

        filmAdapter.setOnItemClickListener(new FilmAdapter.OnItemClickListener(){
            @Override
            public void onItemClick(View v, int position){
                Intent intent = new Intent(MainActivity.this, FilmInfoActivity.class);
                Film currentFilm = films.get(position);
                intent.putExtra(Film.class.getCanonicalName(), currentFilm);
                intent.putExtra("FilmsVersion", filmsVersion.getVersion());
                startActivityForResult(intent, FILM_INFO_ACTIVITY_ID);
            }
        });

        //Применим наш адаптер к RecyclerView
        rvMain.setAdapter(filmAdapter);
        //И установим LayoutManager
        rvMain.setLayoutManager(new LinearLayoutManager(this));

        changeFontSize();
        overrideFont(getApplicationContext(), DEFAULT_FONT, currentFont);
        changeLocale();

        //JSONHelper.exportToJSON(this, getFilms());

    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data){
        if(requestCode == FILM_INFO_ACTIVITY_ID){
            if(resultCode == RESULT_OK){
                long newVersion = data.getLongExtra("FilmsVersion", 0);
                filmsVersion.setVersion(newVersion);

                final Film changedFilm = data.getParcelableExtra(Film.class.getCanonicalName());
                Film oldFilm = null;

                for (Film film : films)
                    if (film.getId().equals(changedFilm.getId()))
                        oldFilm = film;

                if (oldFilm != null) {
                    int index = films.indexOf(oldFilm);
                    films.set(index, changedFilm);
                }

                //redrawAdapter(films);
            }
        }
        else{
            super.onActivityResult(requestCode, resultCode, data);
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.all_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case R.id.action_settings:
                showSettingsDialog();
                break;

            case R.id.action_filter:
                showFilterDialog();
                break;

            case R.id.action_add_film:
                showFilmAddingDialog();
                break;
        }

        return true;
    }

    public void showSettingsDialog() {

        if (settingsDialogFragment == null)
            settingsDialogFragment = new SettingsDialogFragment();

        settingsDialogFragment.show(getSupportFragmentManager(), "settings");
    }

    public void showFilterDialog() {

        if (filterDialogFragment == null)
            filterDialogFragment = new FilterDialogFragment();

        filterDialogFragment.show(getSupportFragmentManager(), "filter");
    }

    public void showFilmAddingDialog() {

        if (filmAddingDialogFragment == null)
            filmAddingDialogFragment = new FilmAddingDialogFragment();

        filmAddingDialogFragment.show(getSupportFragmentManager(), "adding film");
    }

    @Override
    public void onSettingsDialogPositiveClick(DialogFragment dialog) {

        SettingsDialogFragment myDialogFragment = (SettingsDialogFragment) dialog;

        int newScale = myDialogFragment.getCurrentFontSize();

        if (newScale != currentTextSizeScale) {
            currentTextSizeScale = newScale;
            changeFontSize();

            redrawAdapter(films);
        }

        String newFont = myDialogFragment.getCurrentFont();

        if ((newFont != null) && (!newFont.equals(currentFont))) {
            currentFont = newFont;
            overrideFont(getApplicationContext(), DEFAULT_FONT, currentFont);

            redrawAdapter(films);
        }

        String newLocale = myDialogFragment.getCurrentLocale();

        if ((newLocale != null) && (!newLocale.equals(currentLocale))) {
            currentLocale = newLocale;
            changeLocale();

            //redrawAdapter(films);
        }

    }

    @Override
    public void onSettingsDialogNegativeClick(DialogFragment dialog) {


    }


    @Override
    public void onFilterDialogPositiveClick(DialogFragment dialog) {
        FilterDialogFragment myDialogFragment = (FilterDialogFragment) dialog;

        if(myDialogFragment.getFilteringViews()) {
            long newValueStart = myDialogFragment.getCurrentStartRangeInt();
            long newValueEnd = myDialogFragment.getCurrentEndRangeInt();

            if (newValueEnd < newValueStart)
                newValueEnd = Long.MAX_VALUE;

            if (newValueStart != currentFilterViewsStartRange || newValueEnd != currentFilterViewsEndRange || wasAllFilmsRestart) {
                currentFilterViewsStartRange = newValueStart;
                currentFilterViewsEndRange = newValueEnd;
                wasAllFilmsRestart = false;

                currentFilms = filterFilmsByViews(films, currentFilterViewsStartRange, currentFilterViewsEndRange);
                redrawAdapter(currentFilms);
            }
        }

        if (myDialogFragment.getSortingNamesAscending()) {
            wasAllFilmsRestart = false;
            ascendingSortFilmsByName(currentFilms);
            redrawAdapter(currentFilms);
        }

        if (myDialogFragment.getSortingNameDescending()) {
            wasAllFilmsRestart = false;
            descendingSortFilmsByName(currentFilms);
            redrawAdapter(currentFilms);
        }

        if (myDialogFragment.getSortingRatingsAscending()) {
            wasAllFilmsRestart = false;
            ascendingSortFilmsByRating(currentFilms);
            redrawAdapter(currentFilms);
        }

        if (myDialogFragment.getSortingRatingsDescending()) {
            wasAllFilmsRestart = false;
            descendingSortFilmsByRating(currentFilms);
            redrawAdapter(currentFilms);
        }

        if (myDialogFragment.getRestartAllFilms() && !wasAllFilmsRestart) {
            wasAllFilmsRestart = true;
            currentFilms = new ArrayList<>(films);
            redrawAdapter(currentFilms);
        }
    }

    @Override
    public void onFilterDialogNegativeClick(DialogFragment dialog) {

    }


    @Override
    public void onFilmAddingDialogPositiveClick(DialogFragment dialog) {
        final FilmAddingDialogFragment myDialogFragment = (FilmAddingDialogFragment) dialog;

        if(!CheckFilmAddingDialogResult(myDialogFragment))
            return;

        final String[] trailerURL = {myDialogFragment.getCurrentFilmTrailerURI()};

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
                    getString(R.string.main_activity_film_adding_start_uploading_trailer),
                    Toast.LENGTH_SHORT);
            toast.show();

            // Register observers to listen for when the download is done or if it fails
            uploadTask.addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception exception) {
                    // Handle unsuccessful uploads
                    Toast toast = Toast.makeText(getApplicationContext(),
                            getString(R.string.main_activity_film_adding_failure_uploaded_trailer),
                            Toast.LENGTH_SHORT);
                    toast.show();
                }
            }).addOnSuccessListener(new OnSuccessListener<UploadTask.TaskSnapshot>() {
                @Override
                public void onSuccess(UploadTask.TaskSnapshot taskSnapshot) {
                    // taskSnapshot.getMetadata() contains file metadata such as size, content-type, etc.
                    // ...
                    trailerURL[0] = taskSnapshot.getUploadSessionUri().toString();
                    Toast toast = Toast.makeText(getApplicationContext(),
                            getString(R.string.main_activity_film_adding_success_uploaded_trailer),
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
                                trailerURL[0] = task.getResult().toString();

                                addFilm(myDialogFragment, trailerURL[0]);
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
            addFilm(myDialogFragment, trailerURL[0]);
        }
    }


    @Override
    public void onFilmAddingDialogNegativeClick(DialogFragment dialog) {


    }


    private void overrideFont(Context context, String defaultFontNameToOverride, String customFontFileNameInAssets) {
        try {
            final Typeface customFontTypeface = Typeface.createFromAsset(context.getAssets(), customFontFileNameInAssets);
            final Field defaultFontTypefaceField = Typeface.class.getDeclaredField(defaultFontNameToOverride);
            defaultFontTypefaceField.setAccessible(true);
            defaultFontTypefaceField.set(null, customFontTypeface);
        } catch (Exception e) {
            Log.e("Font","Can not set custom font " + customFontFileNameInAssets + " instead of " + defaultFontNameToOverride);
        }
    }

    private void changeFontSize() {
        //активируем параметры
        SharedPreferences settings = getSharedPreferences("com.example.Kinopoisk_10", MODE_PRIVATE);
        SharedPreferences.Editor value_add = settings.edit();
        //заносим новый коэффициент увеличения шрифта
        value_add.putInt("currentTextSizeScale", currentTextSizeScale);
        value_add.apply();

        //устанавливаем размер шрифта в приложении
        Resources res = getResources();
        float сoef = TEXT_SIZE_START_VALUE + currentTextSizeScale * TEXT_SIZE_STEP; //новый коэффициент увеличения шрифта
        Configuration configuration = new Configuration(res.getConfiguration());
        configuration.fontScale = сoef;
        res.updateConfiguration(configuration, res.getDisplayMetrics());
    }

    private void changeLocale() {
        Resources resources = getResources();
        Configuration configuration = new Configuration(resources.getConfiguration());
        configuration.setLocale(new Locale(currentLocale));
        resources.updateConfiguration(configuration, resources.getDisplayMetrics());
    }

    private ArrayList<Film> filterFilmsByViews(ArrayList<Film> filmsList, final long startRange, final long endRange) {
        ArrayList<Film> newFilmsList = new ArrayList<>();
        for(Film film : filmsList) {
            if (film.getViews() >= startRange && film.getViews() <= endRange)
                newFilmsList.add(film);
        }
        return newFilmsList;
    }

    private void ascendingSortFilmsByName(ArrayList<Film> filmsList) {
        Collections.sort(filmsList, new Comparator<Film>() {
            public int compare(Film o1, Film o2) {
                return o1.getName().compareTo(o2.getName());
            }
        });
    }

    private void descendingSortFilmsByName(ArrayList<Film> filmsList) {
        Collections.sort(filmsList, new Comparator<Film>() {
            public int compare(Film o1, Film o2) {
                return o2.getName().compareTo(o1.getName());
            }
        });
    }

    private void ascendingSortFilmsByRating(ArrayList<Film> filmsList) {
        Collections.sort(filmsList, new Comparator<Film>() {
            public int compare(Film o1, Film o2) {
                double o1Rating = (double)o1.getRatingsSum() / o1.getViews();
                double o2Rating = (double)o2.getRatingsSum() / o2.getViews();

                if(o1Rating < o2Rating)
                    return -1;
                else if(o1Rating > o2Rating)
                    return 1;
                return o1.getName().compareTo(o2.getName());
            }
        });
    }

    private void descendingSortFilmsByRating(ArrayList<Film> filmsList) {
        Collections.sort(filmsList, new Comparator<Film>() {
            public int compare(Film o1, Film o2) {
                double o1Rating = (double)o1.getRatingsSum() / o1.getViews();
                double o2Rating = (double)o2.getRatingsSum() / o2.getViews();

                if(o2Rating < o1Rating)
                    return -1;
                else if(o2Rating > o1Rating)
                    return 1;
                return o1.getName().compareTo(o2.getName());
            }
        });
    }

    private ArrayList<Film> getFilms() {
        return JSONHelper.importFromJSON(this);
    }

    private void redrawAdapter(ArrayList<Film> filmsList) {
        filmAdapter = new FilmAdapter(getApplicationContext(), filmsList);
        rvMain.setAdapter(filmAdapter);
        rvMain.setLayoutManager(new LinearLayoutManager(this));
    }

    private Boolean CheckFilmAddingDialogResult(FilmAddingDialogFragment myDialogFragment) {
        if (!myDialogFragment.getIsAddingFilm())
            return false;

        Toast toast;

        if (myDialogFragment.getCurrentFilmName().isEmpty()) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_name_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmDescription().isEmpty()) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_description_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmTagLine().isEmpty()) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_tag_line_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmDirector().isEmpty()) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_director_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmGenres().isEmpty()) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_genres_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmReleaseYear() == 0) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_release_year_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmDuration() == 0) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_duration_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmViews() == 0) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_views_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmRatingsSum() == 0) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_ratings_sum_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        if (myDialogFragment.getCurrentFilmPosterURI().isEmpty()) {
            toast = Toast.makeText(getApplicationContext(),
                    getString(R.string.main_activity_film_adding_toast_empty, getString(R.string.film_adding_label_poster_URI_string)),
                    Toast.LENGTH_SHORT);
            toast.show();
            return false;
        }

        return true;
    }

    private String genresToString(String[] genres) {
        String result = "";
        for (int i = 0; i < genres.length; i++) {
            result += genres[i];
            if (i != genres.length - 1)
                result += ", ";
        }
        return result;
    }

    /////////////

    private void addFilm(FilmAddingDialogFragment myDialogFragment, String trailerURL){

        Film film = new Film(myDialogFragment.getCurrentFilmName(),
                myDialogFragment.getCurrentFilmDescription(),
                myDialogFragment.getCurrentFilmTagLine(),
                myDialogFragment.getCurrentFilmDirector(),
                myDialogFragment.getCurrentFilmGenresArrayList(),
                myDialogFragment.getCurrentFilmReleaseYear(),
                myDialogFragment.getCurrentFilmDuration(),
                myDialogFragment.getCurrentFilmViews(),
                myDialogFragment.getCurrentFilmRatingsSum(),
                myDialogFragment.getCurrentFilmPosterURI(),
                trailerURL);

        films.add(film);
        redrawAdapter(films);

        Toast toast = Toast.makeText(getApplicationContext(),
                getString(R.string.main_activity_film_adding_toast_success),
                Toast.LENGTH_SHORT);
        toast.show();

        //JSONHelper.exportToJSON(getApplicationContext(), films);

        filmsVersion.setVersion(filmsVersion.getVersion() + 1);

        FirebaseHelper.setVersion(mDatabaseReference, filmsVersion.getVersion());
        RealmHelper.updateFilmsVersion(mRealm, filmsVersion);

        FirebaseHelper.addFilm(mDatabaseReference, film);
        RealmHelper.addFilm(mRealm, film);

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

    /*
    private void restartApp() {

        finish();
        overridePendingTransition(0, 0);
        startActivity(getIntent());
        overridePendingTransition(0, 0);

    }
    */
}

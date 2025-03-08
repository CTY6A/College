package com.example.kinopoisk_10;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;

import com.google.firebase.FirebaseApp;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;

import java.util.ArrayList;

import io.realm.Realm;

public class StartActivity extends AppCompatActivity {

    private static final long CURR_VERSION = 0;

    private FilmsVersion filmsVersion;
    private ArrayList<Film> films;

    private FirebaseDatabase mFirebaseDatabase;
    private DatabaseReference mDatabaseReference;

    private Realm mRealm;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        //setTheme(R.style.AppTheme);

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start);

        initFirebase();

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

                                            RealmHelper.removeAllFilms(mRealm);

                                            for (Film film : films)
                                                RealmHelper.addFilm(mRealm, film);

                                            startMainActivity();
                                        }

                                        @Override
                                        public void onCancelled(DatabaseError databaseError) {

                                        }
                                    });
                        }
                        else {
                            //films = getFilms();
                            films = RealmHelper.getAllFilms(mRealm);

                            //Uncomment below code to make sure that you loaded from Realm
                            //
                            //films.get(0).setName("kek");

                            startMainActivity();
                        }
                    }

                    @Override
                    public void onCancelled(DatabaseError databaseError) {

                    }
                });


    }

    private void startMainActivity() {
        Intent intent = new Intent(StartActivity.this, MainActivity.class);
        intent.putParcelableArrayListExtra("AllFilms", films);
        intent.putExtra("FilmsVersion", filmsVersion.getVersion());
        startActivity(intent);
        finish();
    }

    private ArrayList<Film> getFilms() {
        return JSONHelper.importFromJSON(this);
    }

    private void initFirebase() {
        //инициализируем наше приложение для Firebase согласно параметрам в google-services.json
        // (google-services.json - файл, с настройками для firebase, кот. мы получили во время регистрации)
        FirebaseApp.initializeApp(this);
        //получаем точку входа для базы данных
        mFirebaseDatabase = FirebaseDatabase.getInstance();
        //получаем ссылку для работы с базой данных
        mDatabaseReference = mFirebaseDatabase.getReference();
    }
}

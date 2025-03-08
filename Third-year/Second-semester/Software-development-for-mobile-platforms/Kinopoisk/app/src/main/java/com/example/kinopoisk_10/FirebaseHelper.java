package com.example.kinopoisk_10;

import android.view.View;

import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.ValueEventListener;

import java.util.ArrayList;
import java.util.HashMap;

public final class FirebaseHelper {

    public final static String VERSION_DATA_BASE_NAME = "version";
    public final static String FILMS_DATA_BASE_NAME = "films";

    public static ArrayList<Film> getFilms(DatabaseReference databaseReference) {
        final ArrayList<Film> films = new ArrayList<>();

        databaseReference.child(FILMS_DATA_BASE_NAME)
                .addListenerForSingleValueEvent(new ValueEventListener() {
                    //если данные в БД меняются
                    @Override
                    public void onDataChange(DataSnapshot dataSnapshot) {
                        //проходим по всем записям и помещаем их в list_users в виде класса User
                        for (DataSnapshot postSnapshot : dataSnapshot.getChildren()) {
                            Film film = postSnapshot.getValue(Film.class);
                            films.add(film);
                        }
                    }

                    @Override
                    public void onCancelled(DatabaseError databaseError) {

                    }
                });

        return films;
    }

    public static long getVersion(DatabaseReference databaseReference) {
        final long[] version = {-1};

        databaseReference.child(VERSION_DATA_BASE_NAME)
                .addListenerForSingleValueEvent(new ValueEventListener() {
                    //если данные в БД меняются
                    @Override
                    public void onDataChange(DataSnapshot dataSnapshot) {
                        version[0] = dataSnapshot.getValue(Long.class);
                    }

                    @Override
                    public void onCancelled(DatabaseError databaseError) {

                    }
                });

        return version[0];
    }

    public static void setVersion(DatabaseReference databaseReference, long version) {
        databaseReference.child(VERSION_DATA_BASE_NAME).setValue(version);
    }

    public static void addFilm(DatabaseReference databaseReference, Film film) {
        //сохраняем данные в базе данных Firebase по пути films -> UUID_Film
        databaseReference.child(FILMS_DATA_BASE_NAME).child(film.getId()).setValue(film);
    }

    public static void updateFilm(DatabaseReference databaseReference, Film film) {

        HashMap map = new HashMap();
        map.put(film.getId(), film);

        databaseReference.child(FILMS_DATA_BASE_NAME).updateChildren(map);
    }
}

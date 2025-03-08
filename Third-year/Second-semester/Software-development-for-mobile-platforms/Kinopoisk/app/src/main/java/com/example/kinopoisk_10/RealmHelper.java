package com.example.kinopoisk_10;

import com.google.gson.Gson;

import java.util.ArrayList;

import io.realm.Realm;
import io.realm.RealmResults;

public final class RealmHelper {

    //below happens strange things, because of realmFilmVersion fields
    //can be only changed in Realm transactions and i don't need this
    public static FilmsVersion getFilmsVersion(Realm realm) {
        FilmsVersion realmFilmVersion = realm.where(FilmsVersion.class).findFirst();
        FilmsVersion filmsVersion = new FilmsVersion(realmFilmVersion.getVersion());
        return  filmsVersion;
    }

    public static ArrayList<Film> getAllFilms(Realm realm) {
        ArrayList<RealmFilm> realmFilms = new ArrayList<>(realm.where(RealmFilm.class).findAll());
        ArrayList<Film> films = new ArrayList<>();

        for (RealmFilm realmFilm : realmFilms)
            films.add(RealmFilmHelper.transformRealmFilmToFilm(realmFilm));

        return films;
    }

    public static void addFilmsVersion(Realm realm, FilmsVersion filmsVersion) {
        realm.beginTransaction();
        realm.copyToRealm(filmsVersion);
        realm.commitTransaction();
    }

    public static void updateFilmsVersion(Realm realm, FilmsVersion filmsVersion) {
        realm.beginTransaction();
        realm.where(FilmsVersion.class).findFirst().setVersion(filmsVersion.getVersion());
        realm.commitTransaction();
    }

    public static void addFilm(Realm realm, Film film) {
        realm.beginTransaction();
        realm.copyToRealm(RealmFilmHelper.transformFilmToRealmFilm(film));
        realm.commitTransaction();
    }

    public static void updateFilm(Realm realm, Film film) {
        realm.beginTransaction();
        realm.copyToRealmOrUpdate(RealmFilmHelper.transformFilmToRealmFilm(film));
        realm.commitTransaction();
    }

    public static void removeAllFilmsVersions(Realm realm) {
        RealmResults<FilmsVersion> result = realm.where(FilmsVersion.class).findAll();
        realm.beginTransaction();
        result.clear();
        realm.commitTransaction();
    }

    public static void removeAllFilms(Realm realm) {
        RealmResults<RealmFilm> result = realm.where(RealmFilm.class).findAll();
        realm.beginTransaction();
        result.clear();
        realm.commitTransaction();
    }
}

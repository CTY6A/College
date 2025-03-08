package com.example.kinopoisk_10;

import java.util.ArrayList;
import java.util.Arrays;

public final class RealmFilmHelper {

    public static Film transformRealmFilmToFilm(RealmFilm realmFilm) {
        Film film = new Film(realmFilm.getName(),
                             realmFilm.getDescription(),
                             realmFilm.getTagLine(),
                             realmFilm.getDirector(),
                             transformStringToArrayList(realmFilm.getGenres()),
                             realmFilm.getReleaseYear(),
                             realmFilm.getDuration(),
                             realmFilm.getViews(),
                             realmFilm.getRatingsSum(),
                             realmFilm.getPosterURI(),
                             realmFilm.getTrailerURI());

        film.setId(realmFilm.getId());

        return  film;
    }

    public static RealmFilm transformFilmToRealmFilm(Film film) {
        RealmFilm realmFilm = new RealmFilm(film.getName(),
                                            film.getDescription(),
                                            film.getTagLine(),
                                            film.getDirector(),
                                            transformArrayListToString(film.getGenres()),
                                            film.getReleaseYear(),
                                            film.getDuration(),
                                            film.getViews(),
                                            film.getRatingsSum(),
                                            film.getPosterURI(),
                                            film.getTrailerURI());

        realmFilm.setId(film.getId());

        return  realmFilm;
    }

    private static ArrayList<String> transformStringToArrayList(String data) {
        return  new ArrayList<>(Arrays.asList(data.split(" ")));
    }

    private static String transformArrayListToString(ArrayList<String> arrayList) {
        String result = "";
        for (String item : arrayList)
            result += item + " ";

        result = result.substring(0, result.length() - 1);
        return  result;
    }
}

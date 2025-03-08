package com.example.kinopoisk_10;

import java.util.ArrayList;
import java.util.UUID;

import io.realm.Realm;
import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
import io.realm.annotations.Required;

public class RealmFilm extends RealmObject {

    @PrimaryKey
    @Required
    private String id;
    @Required
    private String name;
    @Required
    private String description;
    @Required
    private String tagLine;
    @Required
    private String director;
    @Required
    private String genres;
    private int releaseYear;
    private int duration;
    private long views;
    private long ratingsSum;
    @Required
    private String posterURI;
    private String trailerURI;


    public String getId() { return id; }

    public String getName() { return name; }

    public String getDescription() {
        return description;
    }

    public String getTagLine() {
        return tagLine;
    }

    public String getDirector() { return director; }

    public String getGenres() { return genres; }

    public int getReleaseYear() {
        return releaseYear;
    }

    public int getDuration() {
        return duration;
    }

    public long getViews() {
        return views;
    }

    public long getRatingsSum() {
        return ratingsSum;
    }

    public String getPosterURI() { return posterURI; }

    public String getTrailerURI() { return trailerURI; }

    public void setId(String id) { this.id = id; }

    public void setName(String name) { this.name = name; }

    public void setDescription(String description) { this.description = description; }

    public void setTagLine(String tagLine) { this.tagLine = tagLine; }

    public void setDirector(String _director) { this.director = _director; }

    public void setGenres(String genres) { this.genres = genres; }

    public void setReleaseYear(int releaseYear) { this.releaseYear = releaseYear; }

    public void setDuration(int duration) { this.duration = duration; }

    public void setViews(long views) { this.views = views; }

    public void setRatingsSum(long ratingsSum) { this.ratingsSum = ratingsSum; }

    public void setPosterURI(String posterURI) { this.posterURI = posterURI; }

    public void setTrailerURI(String trailerURI) { this.trailerURI = trailerURI; }

    public RealmFilm() { }

    public RealmFilm(String name, String description, String tagLine, String director, String genres,
                int releaseYear, int duration, long views, long ratingsSum, String posterURI) {
        this.id = UUID.randomUUID().toString();
        this.name = name;
        this.description = description;
        this.tagLine = tagLine;
        this.director = director;
        this.genres = genres;
        this.releaseYear = releaseYear;
        this.duration = duration;
        this.views = views;
        this.ratingsSum = ratingsSum;
        this.posterURI = posterURI;
        this.trailerURI = null;
    }

    public RealmFilm(String name, String description, String tagLine, String director, String genres,
                int releaseYear, int duration, long views, long ratingsSum, String posterURI, String trailerURI) {

        this(name, description, tagLine, director, genres, releaseYear, duration, views, ratingsSum, posterURI);

        this.trailerURI = trailerURI;
    }
}

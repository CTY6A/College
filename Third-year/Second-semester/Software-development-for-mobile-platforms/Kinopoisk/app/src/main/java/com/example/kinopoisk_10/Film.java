package com.example.kinopoisk_10;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.firebase.database.Exclude;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.UUID;

import io.realm.RealmObject;
import io.realm.annotations.Required;

public class Film implements Parcelable {

    private String id;
    private String name;
    private String description;
    private String tagLine;
    private String director;
    private ArrayList<String> genres;
    private int releaseYear;
    private int duration;
    private long views;
    private long ratingsSum;
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

    public ArrayList<String> getGenres() { return genres; }

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

    public void setGenres(ArrayList<String> genres) { this.genres = genres; }

    public void setReleaseYear(int releaseYear) { this.releaseYear = releaseYear; }

    public void setDuration(int duration) { this.duration = duration; }

    public void setViews(long views) { this.views = views; }

    public void setRatingsSum(long ratingsSum) { this.ratingsSum = ratingsSum; }

    public void setPosterURI(String posterURI) { this.posterURI = posterURI; }

    public void setTrailerURI(String trailerURI) { this.trailerURI = trailerURI; }


    public Film() {}

    public Film(String name, String description, String tagLine, String director, ArrayList<String> genres,
                int releaseYear, int duration, long views, long ratingsSum, String posterURI) {
        this.id = UUID.randomUUID().toString();
        this.name = name;
        this.description = description;
        this.tagLine = tagLine;
        this.director = director;
        this.genres = new ArrayList<>(genres);
        this.releaseYear = releaseYear;
        this.duration = duration;
        this.views = views;
        this.ratingsSum = ratingsSum;
        this.posterURI = posterURI;
        this.trailerURI = null;
    }

    public Film(String name, String description, String tagLine, String director, ArrayList<String> genres,
                int releaseYear, int duration, long views, long ratingsSum, String posterURI, String trailerURI) {

        this(name, description, tagLine, director, genres, releaseYear, duration, views, ratingsSum, posterURI);

        this.trailerURI = trailerURI;
    }

    @Override
    public int describeContents() {
        return 0;
    }

    // упаковываем объект в Parcel
    public void writeToParcel(Parcel parcel, int flags) {
        parcel.writeString(id);
        parcel.writeString(name);
        parcel.writeString(description);
        parcel.writeString(tagLine);
        parcel.writeString(director);
        parcel.writeStringList(genres);
        parcel.writeInt(releaseYear);
        parcel.writeInt(duration);
        parcel.writeLong(views);
        parcel.writeLong(ratingsSum);
        parcel.writeString(posterURI);
        parcel.writeString(trailerURI);
    }

    public static final Parcelable.Creator<Film> CREATOR = new Parcelable.Creator<Film>() {
        // распаковываем объект из Parcel
        public Film createFromParcel(Parcel in) {
            return new Film(in);
        }

        public Film[] newArray(int size) {
            return new Film[size];
        }
    };

    // конструктор, считывающий данные из Parcel
    private Film(Parcel parcel) {
       id = parcel.readString();
        name = parcel.readString();
        description = parcel.readString();
        tagLine = parcel.readString();
        director = parcel.readString();
        genres = parcel.createStringArrayList();
        releaseYear = parcel.readInt();
        duration = parcel.readInt();
        views = parcel.readLong();
        ratingsSum = parcel.readLong();
        posterURI = parcel.readString();
        trailerURI = parcel.readString();
    }

}

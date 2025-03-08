package com.example.kinopoisk_10;

import android.content.Context;
import android.provider.SyncStateContract;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Writer;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class JSONHelper {

    private static final String FILE_NAME = "films.json";

    static boolean exportToJSON(Context context, ArrayList<Film> dataList) {

        Gson gson = new Gson();
        DataItems dataItems = new DataItems();
        dataItems.setFilms(dataList);

        String jsonString = gson.toJson(dataItems);

        FileOutputStream fileOutputStream = null;

        try {
            fileOutputStream = new FileOutputStream(new File(Objects.requireNonNull(context.getExternalCacheDir()).getAbsolutePath() + "/" + FILE_NAME));
            fileOutputStream.write(jsonString.getBytes());
            return true;
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (fileOutputStream != null) {
                try {
                    fileOutputStream.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

/*
        DataItems dataItems = new DataItems();
        dataItems.setFilms(dataList);

        try {
            // create writer
            Writer writer = new FileWriter("Films.json");

            // convert users list to JSON file
            new Gson().toJson(dataItems, writer);

            // close writer
            writer.close();

            return true;
        }
        catch (Exception e) {
            e.printStackTrace();
        }
        */

        return false;

    }

    static ArrayList<Film> importFromJSON(Context context) {

        InputStreamReader streamReader = null;
        InputStream fileInputStream = null;
        //FileInputStream fileInputStream = null;
        try{
            File file = new File(Objects.requireNonNull(context.getExternalCacheDir()).getAbsolutePath() + "/" + FILE_NAME);

            if (file.exists())
                fileInputStream = new FileInputStream(file);
            else
                fileInputStream = context.getAssets().open(FILE_NAME);

            //fileInputStream = context.openFileInput(FILE_NAME);
            streamReader = new InputStreamReader(fileInputStream);
            Gson gson = new Gson();
            DataItems dataItems = gson.fromJson(streamReader, DataItems.class);
            return  dataItems.getFilms();
        }
        catch (IOException ex){
            ex.printStackTrace();
        }
        finally {
            if (streamReader != null) {
                try {
                    streamReader.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            if (fileInputStream != null) {
                try {
                    fileInputStream.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

        return null;
    }

    private static class DataItems {
        private ArrayList<Film> films;

        ArrayList<Film> getFilms() {
            return films;
        }
        void setFilms(ArrayList<Film> films) {
            this.films = films;
        }
    }
}

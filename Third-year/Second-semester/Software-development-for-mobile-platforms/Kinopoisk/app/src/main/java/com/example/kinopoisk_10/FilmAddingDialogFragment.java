package com.example.kinopoisk_10;

import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;

import androidx.appcompat.app.AlertDialog;
import androidx.fragment.app.DialogFragment;

import java.util.ArrayList;
import java.util.Objects;

public class FilmAddingDialogFragment extends DialogFragment {

    private static final boolean DEFAULT_ADD_FILM_CHECKBOX_STATE = false;

    private static final String DEFAULT_NAME_EDIT_TEXT = "Нападение помидоров-убийц";
    private static final String DEFAULT_DESCRIPTION_EDIT_TEXT = "Все население земного шара охвачено паникой. Невинные помидоры, которые люди повсеместно выращивали у себя в огородах, неожиданно превратились в неутомимых охотников, жаждущих человеческого мяса. Полиция и армия не в силах справиться с угрозой и только песня, посвященная подростковому сексу, спасет Землю от кровожадных людоедов.";
    private static final String DEFAULT_TAG_LINE_EDIT_TEXT = "Aaaргх!";
    private static final String DEFAULT_DIRECTOR_EDIT_TEXT = "Джон Де Белло";
    private static final String DEFAULT_GENRES_EDIT_TEXT = "ужасы,фантастика,комедия";

    private static final int DEFAULT_RELEASE_YEAR_EDIT_TEXT = 1978;
    private static final int DEFAULT_DURATION_EDIT_TEXT = 83;

    private static final long DEFAULT_VIEWS_EDIT_TEXT = 1714;
    private static final long DEFAULT_RATING_SUM_EDIT_TEXT = 8947;

    private static final String DEFAULT_POSTER_URI_EDIT_TEXT = "https://firebasestorage.googleapis.com/v0/b/kinopoisk-a295e.appspot.com/o/Posters%2F%D0%9D%D0%B0%D0%BF%D0%B0%D0%B4%D0%B5%D0%BD%D0%B8%D0%B5%20%D0%BF%D0%BE%D0%BC%D0%B8%D0%B4%D0%BE%D1%80%D0%BE%D0%B2-%D1%83%D0%B1%D0%B8%D0%B9%D1%86%20(1978).webp?alt=media&token=b373fe3e-70ee-4133-b602-23bf2f9abc85";

    private static final String DEFAULT_TRAILER_LOCAL_FILE_EDIT_TEXT = "Нападение помидоров-убийц.mp4";
    private static final Boolean DEFAULT_IS_TRAILER_LOCAL_FILE_CHECKBOX_STATE = false;

    private static final String DEFAULT_TRAILER_URI_EDIT_TEXT = "https://firebasestorage.googleapis.com/v0/b/kinopoisk-a295e.appspot.com/o/Trailers%2F%D0%9D%D0%B0%D0%BF%D0%B0%D0%B4%D0%B5%D0%BD%D0%B8%D0%B5%20%D0%BF%D0%BE%D0%BC%D0%B8%D0%B4%D0%BE%D1%80%D0%BE%D0%B2-%D1%83%D0%B1%D0%B8%D0%B9%D1%86%20(1978).mp4?alt=media&token=488f30a6-d5ad-45a9-8bb8-6c8c39300db4";
    private static final Boolean DEFAULT_IS_TRAILER_URI_CHECKBOX_STATE = true;

    private static final Boolean DEFAULT_IS_ADDING_FILM_CHECKBOX_STATE = false;

    private String currentFilmName;
    private String currentFilmDescription;
    private String currentFilmTagLine;
    private String currentFilmDirector;
    private String currentFilmGenres;

    private int currentFilmReleaseYear;
    private String currentFilmReleaseYearString;
    private int currentFilmDuration;
    private String currentFilmDurationString;

    private long currentFilmViews;
    private String currentFilmViewsString;
    private long currentFilmRatingsSum;
    private String currentFilmRatingsSumString;

    private String currentFilmPosterURI;

    private String currentFilmTrailerLocalFile;
    private Boolean isTrailerLocalFile;

    private String currentFilmTrailerURI;
    private Boolean isTrailerURI;

    private ArrayList<String> currentFilmGenresArrayList;

    private Boolean isAddingFilm;


    public String getCurrentFilmName() { return currentFilmName; }

    public String getCurrentFilmDescription() { return currentFilmDescription; }

    public String getCurrentFilmTagLine() { return currentFilmTagLine; }

    public String getCurrentFilmDirector() { return currentFilmDirector; }

    public String getCurrentFilmGenres() { return currentFilmGenres; }

    public int getCurrentFilmReleaseYear() { return currentFilmReleaseYear; }

    public int getCurrentFilmDuration() { return currentFilmDuration; }

    public long getCurrentFilmViews() { return currentFilmViews; }

    public long getCurrentFilmRatingsSum() { return currentFilmRatingsSum; }

    public String getCurrentFilmPosterURI() { return currentFilmPosterURI; }

    public String getCurrentFilmTrailerLocalFile() { return currentFilmTrailerLocalFile; }

    public Boolean getIsTrailerLocalFile() { return isTrailerLocalFile; }

    public String getCurrentFilmTrailerURI() { return currentFilmTrailerURI; }

    public Boolean getIsTrailerURI() { return isTrailerURI; }

    public ArrayList<String> getCurrentFilmGenresArrayList() { return currentFilmGenresArrayList; }

    public Boolean getIsAddingFilm() { return  isAddingFilm; }


    public FilmAddingDialogFragment() {
        currentFilmName = DEFAULT_NAME_EDIT_TEXT;
        currentFilmDescription = DEFAULT_DESCRIPTION_EDIT_TEXT;
        currentFilmTagLine = DEFAULT_TAG_LINE_EDIT_TEXT;
        currentFilmDirector = DEFAULT_DIRECTOR_EDIT_TEXT;
        currentFilmGenres = DEFAULT_GENRES_EDIT_TEXT;

        currentFilmReleaseYear = DEFAULT_RELEASE_YEAR_EDIT_TEXT;
        currentFilmReleaseYearString = Integer.toString(currentFilmReleaseYear);
        currentFilmDuration = DEFAULT_DURATION_EDIT_TEXT;
        currentFilmDurationString = Integer.toString(currentFilmDuration);

        currentFilmViews = DEFAULT_VIEWS_EDIT_TEXT;
        currentFilmViewsString = Long.toString(currentFilmViews);
        currentFilmRatingsSum = DEFAULT_RATING_SUM_EDIT_TEXT;
        currentFilmRatingsSumString = Long.toString(currentFilmRatingsSum);

        currentFilmPosterURI = DEFAULT_POSTER_URI_EDIT_TEXT;

        currentFilmTrailerLocalFile = DEFAULT_TRAILER_LOCAL_FILE_EDIT_TEXT;
        isTrailerLocalFile = DEFAULT_IS_TRAILER_LOCAL_FILE_CHECKBOX_STATE;

        currentFilmTrailerURI = DEFAULT_TRAILER_URI_EDIT_TEXT;
        isTrailerURI = DEFAULT_IS_TRAILER_URI_CHECKBOX_STATE;

        isAddingFilm = DEFAULT_IS_ADDING_FILM_CHECKBOX_STATE;

        currentFilmGenresArrayList = null;

        getGenresStringToArray();
    }


    public interface FilmAddingNoticeDialogListener {
        void onFilmAddingDialogPositiveClick(DialogFragment dialog);
        void onFilmAddingDialogNegativeClick(DialogFragment dialog);
    }

    private FilmAddingNoticeDialogListener _listener;

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        // Verify that the host activity implements the callback interface
        try {
            // Instantiate the NoticeDialogListener so we can send events to the host
            _listener = (FilmAddingNoticeDialogListener) activity;
        } catch (ClassCastException e) {
            // The activity doesn't implement the interface, throw exception
            throw new ClassCastException(activity.toString()
                    + " must implement NoticeDialogListener");
        }
    }

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        AlertDialog.Builder builder = new AlertDialog.Builder(Objects.requireNonNull(getActivity()));
        // Get the layout inflater
        LayoutInflater inflater = getActivity().getLayoutInflater();

        View currentView = inflater.inflate(R.layout.film_adding_item_constraint, null);

        final EditText nameEditText = currentView.findViewById(R.id.film_adding_edit_text_name);
        final EditText descriptionEditText = currentView.findViewById(R.id.film_adding_edit_text_description);
        final EditText tagLineEditText = currentView.findViewById(R.id.film_adding_edit_text_tag_line);
        final EditText directorEditText = currentView.findViewById(R.id.film_adding_edit_text_director);
        final EditText genresEditText = currentView.findViewById(R.id.film_adding_edit_text_genres);
        final EditText releaseYearEditText = currentView.findViewById(R.id.film_adding_edit_text_release_year);
        final EditText durationEditText = currentView.findViewById(R.id.film_adding_edit_text_duration);
        final EditText viewsEditText = currentView.findViewById(R.id.film_adding_edit_text_views);
        final EditText ratingSumEditText = currentView.findViewById(R.id.film_adding_edit_text_ratings_sum);
        final EditText posterURIEditText = currentView.findViewById(R.id.film_adding_edit_text_poster_URI);

        final EditText trailerLocalFileEditText = currentView.findViewById(R.id.film_adding_edit_text_trailer_local_file);
        final CheckBox isTrailerLocalFileCheckBox = currentView.findViewById(R.id.film_adding_checkbox_trailer_local_file);

        final EditText trailerURIEditText = currentView.findViewById(R.id.film_adding_edit_text_trailer_URI);
        final CheckBox isTrailerURICheckBox = currentView.findViewById(R.id.film_adding_checkbox_trailer_URI);

        final CheckBox isAddingFilmCheckBox = currentView.findViewById(R.id.film_adding_checkbox_add_film);

        nameEditText.setText(currentFilmName);
        descriptionEditText.setText(currentFilmDescription);
        tagLineEditText.setText(currentFilmTagLine);
        directorEditText.setText(currentFilmDirector);
        genresEditText.setText(currentFilmGenres);
        releaseYearEditText.setText(currentFilmReleaseYearString);
        durationEditText.setText(currentFilmDurationString);
        viewsEditText.setText(currentFilmViewsString);
        ratingSumEditText.setText(currentFilmRatingsSumString);
        posterURIEditText.setText(currentFilmPosterURI);

        trailerLocalFileEditText.setText(currentFilmTrailerLocalFile);
        isTrailerLocalFileCheckBox.setChecked(isTrailerLocalFile);

        trailerURIEditText.setText(currentFilmTrailerURI);
        isTrailerURICheckBox.setChecked(isTrailerURI);

        isAddingFilmCheckBox.setChecked(isAddingFilm);

        ///
        isTrailerLocalFileCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    isTrailerURICheckBox.setChecked(false);
                }
            }
        });

        ///
        isTrailerURICheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    isTrailerLocalFileCheckBox.setChecked(false);
                }
            }
        });

        // Inflate and set the layout for the dialog
        // Pass null as the parent view because its going in the dialog layout
        builder.setView(currentView)
                // Add action buttons
                .setPositiveButton(R.string.positive_push, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int id) {

                        currentFilmName = nameEditText.getText().toString();
                        currentFilmDescription = descriptionEditText.getText().toString();
                        currentFilmTagLine = tagLineEditText.getText().toString();
                        currentFilmDirector = directorEditText.getText().toString();
                        currentFilmGenres = genresEditText.getText().toString();

                        getGenresStringToArray();

                        currentFilmReleaseYearString = releaseYearEditText.getText().toString();

                        if (currentFilmReleaseYearString.isEmpty())
                            currentFilmReleaseYear = 0;

                        currentFilmDurationString = durationEditText.getText().toString();

                        if (currentFilmDurationString.isEmpty())
                            currentFilmDuration = 0;

                        currentFilmViewsString = viewsEditText.getText().toString();

                        if (currentFilmViewsString.isEmpty())
                            currentFilmViews = 0;

                        currentFilmRatingsSumString = ratingSumEditText.getText().toString();

                        if (currentFilmRatingsSumString.isEmpty())
                            currentFilmRatingsSum = 0;

                        currentFilmPosterURI = posterURIEditText.getText().toString();

                        currentFilmTrailerLocalFile = trailerLocalFileEditText.getText().toString();
                        isTrailerLocalFile = isTrailerLocalFileCheckBox.isChecked();

                        currentFilmTrailerURI = trailerURIEditText.getText().toString();
                        isTrailerURI = isTrailerURICheckBox.isChecked();

                        isAddingFilm = isAddingFilmCheckBox.isChecked();

                        _listener.onFilmAddingDialogPositiveClick(FilmAddingDialogFragment.this);
                    }
                })
                .setNegativeButton(R.string.negative_push, new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        _listener.onFilmAddingDialogNegativeClick(FilmAddingDialogFragment.this);
                    }
                });

        return builder.create();
    }

    private void getGenresStringToArray() {
        if (currentFilmGenres == null)
            return;

        if (currentFilmGenres.isEmpty())
            return;

        String[] allGenres = currentFilmGenres.split(",");

        currentFilmGenresArrayList = new ArrayList<>();

        for (int i = 0; i < allGenres.length; i++) {
            if (allGenres[i] != null && !allGenres[i].isEmpty()) {
                currentFilmGenresArrayList.add(allGenres[i]);
            }
        }

    }

}

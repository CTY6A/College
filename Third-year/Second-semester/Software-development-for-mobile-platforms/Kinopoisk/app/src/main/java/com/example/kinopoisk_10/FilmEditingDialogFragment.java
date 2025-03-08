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

import java.util.Objects;

public class FilmEditingDialogFragment extends DialogFragment {

    private static final String DEFAULT_TRAILER_LOCAL_FILE_EDIT_TEXT = "Нападение помидоров-убийц.mp4";
    private static final Boolean DEFAULT_IS_TRAILER_LOCAL_FILE_CHECKBOX_STATE = false;

    private static final String DEFAULT_TRAILER_URI_EDIT_TEXT = "https://firebasestorage.googleapis.com/v0/b/kinopoisk-a295e.appspot.com/o/Trailers%2F%D0%9D%D0%B0%D0%BF%D0%B0%D0%B4%D0%B5%D0%BD%D0%B8%D0%B5%20%D0%BF%D0%BE%D0%BC%D0%B8%D0%B4%D0%BE%D1%80%D0%BE%D0%B2-%D1%83%D0%B1%D0%B8%D0%B9%D1%86%20(1978).mp4?alt=media&token=488f30a6-d5ad-45a9-8bb8-6c8c39300db4";
    private static final Boolean DEFAULT_IS_TRAILER_URI_CHECKBOX_STATE = true;


    private String currentFilmTrailerLocalFile;
    private Boolean isTrailerLocalFile;

    private String currentFilmTrailerURI;
    private Boolean isTrailerURI;


    public String getCurrentFilmTrailerLocalFile() { return currentFilmTrailerLocalFile; }

    public Boolean getIsTrailerLocalFile() { return isTrailerLocalFile; }

    public String getCurrentFilmTrailerURI() { return currentFilmTrailerURI; }

    public Boolean getIsTrailerURI() { return isTrailerURI; }

    public void setCurrentFilmTrailerURI(String trailerURI) { currentFilmTrailerURI = trailerURI; }

    public  FilmEditingDialogFragment() {
        currentFilmTrailerLocalFile = DEFAULT_TRAILER_LOCAL_FILE_EDIT_TEXT;
        isTrailerLocalFile = DEFAULT_IS_TRAILER_LOCAL_FILE_CHECKBOX_STATE;

        currentFilmTrailerURI = DEFAULT_TRAILER_URI_EDIT_TEXT;
        isTrailerURI = DEFAULT_IS_TRAILER_URI_CHECKBOX_STATE;
    }

    public FilmEditingDialogFragment(String trailerURL) {
        this();
        currentFilmTrailerURI = trailerURL;
    }

    public interface FilmEditingNoticeDialogListener {
        void onFilmEditingDialogPositiveClick(DialogFragment dialog);
        void onFilmEditingDialogNegativeClick(DialogFragment dialog);
    }

    private FilmEditingNoticeDialogListener _listener;

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        // Verify that the host activity implements the callback interface
        try {
            // Instantiate the NoticeDialogListener so we can send events to the host
            _listener = (FilmEditingNoticeDialogListener) activity;
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

        View currentView = inflater.inflate(R.layout.film_editing_item_constraint, null);

        final EditText trailerLocalFileEditText = currentView.findViewById(R.id.film_editing_edit_text_trailer_local_file);
        final CheckBox isTrailerLocalFileCheckBox = currentView.findViewById(R.id.film_editing_checkbox_trailer_local_file);

        final EditText trailerURIEditText = currentView.findViewById(R.id.film_editing_edit_text_trailer_URI);
        final CheckBox isTrailerURICheckBox = currentView.findViewById(R.id.film_editing_checkbox_trailer_URI);

        trailerLocalFileEditText.setText(currentFilmTrailerLocalFile);
        isTrailerLocalFileCheckBox.setChecked(isTrailerLocalFile);

        trailerURIEditText.setText(currentFilmTrailerURI);
        isTrailerURICheckBox.setChecked(isTrailerURI);

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

        builder.setView(currentView)
                // Add action buttons
                .setPositiveButton(R.string.positive_push, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int id) {

                        currentFilmTrailerLocalFile = trailerLocalFileEditText.getText().toString();
                        isTrailerLocalFile = isTrailerLocalFileCheckBox.isChecked();

                        currentFilmTrailerURI = trailerURIEditText.getText().toString();
                        isTrailerURI = isTrailerURICheckBox.isChecked();

                        _listener.onFilmEditingDialogPositiveClick(FilmEditingDialogFragment.this);
                    }
                })
                .setNegativeButton(R.string.negative_push, new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        _listener.onFilmEditingDialogNegativeClick(FilmEditingDialogFragment.this);
                    }
                });

        return builder.create();
    }
}

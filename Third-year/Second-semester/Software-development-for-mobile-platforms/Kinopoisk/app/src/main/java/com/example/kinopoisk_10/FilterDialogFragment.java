package com.example.kinopoisk_10;

import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.SeekBar;
import android.widget.Spinner;

import androidx.appcompat.app.AlertDialog;
import androidx.fragment.app.DialogFragment;

import java.util.Objects;

public class FilterDialogFragment extends DialogFragment {

    private static final boolean DEFAULT_VIEWS_FILTER_CHECKBOX_STATE = false;
    private static final boolean DEFAULT_NAMES_ASCENDING_SORTING_CHECKBOX_STATE = false;
    private static final boolean DEFAULT_NAMES_DESCENDING_SORTING_CHECKBOX_STATE = false;
    private static final boolean DEFAULT_RATINGS_ASCENDING_SORTING_CHECKBOX_STATE = false;
    private static final boolean DEFAULT_RATINGS_DESCENDING_SORTING_CHECKBOX_STATE = false;
    private static final boolean DEFAULT_RESTART_ALL_FILMS_CHECKBOX_STATE = false;

    private static final long DEFAULT_START_RANGE_FILTER_VIEWS = 500;
    private static final long DEFAULT_END_RANGE_FILTER_VIEWS = 1000000;

    private String currentStartRangeString;
    private long currentStartRangeInt;
    private String currentEndRangeString;
    private long currentEndRangeInt;
    private Boolean isFilteringViews;
    private Boolean isSortingNamesAscending;
    private Boolean isSortingNameDescending;
    private Boolean isSortingRatingsAscending;
    private Boolean isSortingRatingsDescending;
    private Boolean isRestartAllFilms;

    public long getCurrentStartRangeInt() {
        return currentStartRangeInt;
    }

    public long getCurrentEndRangeInt() {
        return currentEndRangeInt;
    }

    public Boolean getFilteringViews() {
        return isFilteringViews;
    }

    public Boolean getSortingNamesAscending() {
        return isSortingNamesAscending;
    }

    public Boolean getSortingNameDescending() {
        return isSortingNameDescending;
    }

    public Boolean getSortingRatingsAscending() {
        return isSortingRatingsAscending;
    }

    public Boolean getSortingRatingsDescending() {
        return isSortingRatingsDescending;
    }

    public Boolean getRestartAllFilms() {
        return isRestartAllFilms;
    }

    public FilterDialogFragment() {
        currentStartRangeInt = DEFAULT_START_RANGE_FILTER_VIEWS;
        currentStartRangeString = Long.toString(currentStartRangeInt);

        currentEndRangeInt = DEFAULT_END_RANGE_FILTER_VIEWS;
        currentEndRangeString = Long.toString(currentEndRangeInt);

        isFilteringViews = DEFAULT_VIEWS_FILTER_CHECKBOX_STATE;
        isSortingNamesAscending = DEFAULT_NAMES_ASCENDING_SORTING_CHECKBOX_STATE;
        isSortingNameDescending = DEFAULT_NAMES_DESCENDING_SORTING_CHECKBOX_STATE;
        isSortingRatingsAscending = DEFAULT_RATINGS_ASCENDING_SORTING_CHECKBOX_STATE;
        isSortingRatingsDescending = DEFAULT_RATINGS_DESCENDING_SORTING_CHECKBOX_STATE;
        isRestartAllFilms = DEFAULT_RESTART_ALL_FILMS_CHECKBOX_STATE;
    }

    public FilterDialogFragment(long startRange, long endRange) {
        this();

        currentStartRangeInt = startRange;
        currentStartRangeString = Long.toString(currentStartRangeInt);

        currentEndRangeInt = endRange;
        currentEndRangeString = Long.toString(currentEndRangeInt);
    }

    public interface FilterNoticeDialogListener {
        void onFilterDialogPositiveClick(DialogFragment dialog);
        void onFilterDialogNegativeClick(DialogFragment dialog);
    }

    private FilterNoticeDialogListener _listener;

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        // Verify that the host activity implements the callback interface
        try {
            // Instantiate the NoticeDialogListener so we can send events to the host
            _listener = (FilterNoticeDialogListener) activity;
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

        View currentView = inflater.inflate(R.layout.filter_item_constraint, null);

        final EditText startRangeFilterViews = currentView.findViewById(R.id.edit_text_filter_views_start);
        final EditText endRangeFilterViews = currentView.findViewById(R.id.edit_text_filter_views_end);
        final CheckBox viewsFilterCheckBox = currentView.findViewById(R.id.filter_checkbox_views);
        final CheckBox ascendingNameCheckBox = currentView.findViewById(R.id.filter_checkbox_sorting_name_ascending);
        final CheckBox descendingNameCheckBox = currentView.findViewById(R.id.filter_checkbox_sorting_name_descending);
        final CheckBox ascendingRatingCheckBox = currentView.findViewById(R.id.filter_checkbox_sorting_rating_ascending);
        final CheckBox descendingRatingCheckBox = currentView.findViewById(R.id.filter_checkbox_sorting_rating_descending);
        final CheckBox restartAllFilmsCheckBox = currentView.findViewById(R.id.filter_checkbox_all_films);

        startRangeFilterViews.setText(currentStartRangeString);
        endRangeFilterViews.setText(currentEndRangeString);

        ////
        ascendingNameCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    descendingNameCheckBox.setChecked(false);
                }
            }
        });

        descendingNameCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    ascendingNameCheckBox.setChecked(false);
                }
            }
        });

        ////
        ascendingRatingCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    descendingRatingCheckBox.setChecked(false);
                }
            }
        });

        descendingRatingCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    ascendingRatingCheckBox.setChecked(false);
                }
            }
        });

        ////
        restartAllFilmsCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {

                if (isChecked) {
                    viewsFilterCheckBox.setChecked(false);
                    descendingNameCheckBox.setChecked(false);
                    ascendingNameCheckBox.setChecked(false);
                    ascendingRatingCheckBox.setChecked(false);
                    descendingRatingCheckBox.setChecked(false);
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
                        currentStartRangeString = startRangeFilterViews.getText().toString();

                        if(currentStartRangeString.equals(""))
                            currentEndRangeInt = 0;
                        else
                            currentStartRangeInt = Long.parseLong(currentStartRangeString);

                        currentEndRangeString = endRangeFilterViews.getText().toString();

                        if(currentEndRangeString.equals(""))
                            currentEndRangeInt = 0;
                        else
                            currentEndRangeInt = Long.parseLong(currentEndRangeString);

                        isFilteringViews = viewsFilterCheckBox.isChecked();
                        isSortingNamesAscending = ascendingNameCheckBox.isChecked();
                        isSortingNameDescending = descendingNameCheckBox.isChecked();
                        isSortingRatingsAscending = ascendingRatingCheckBox.isChecked();
                        isSortingRatingsDescending = descendingRatingCheckBox.isChecked();
                        isRestartAllFilms = restartAllFilmsCheckBox.isChecked();

                        _listener.onFilterDialogPositiveClick(FilterDialogFragment.this);
                    }
                })
                .setNegativeButton(R.string.negative_push, new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        _listener.onFilterDialogNegativeClick(FilterDialogFragment.this);
                    }
                });

        return builder.create();
    }
}

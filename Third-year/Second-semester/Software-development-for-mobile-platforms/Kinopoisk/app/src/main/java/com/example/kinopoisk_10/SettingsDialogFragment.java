package com.example.kinopoisk_10;

import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.os.Environment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.SeekBar;
import android.widget.Spinner;

import androidx.appcompat.app.AlertDialog;
import androidx.fragment.app.DialogFragment;

import java.util.ArrayList;
import java.util.LinkedHashMap;

public class SettingsDialogFragment extends DialogFragment {

    private static final int DEFAULT_TEXT_SIZE_SCALE = 4;
    private static final int DEFAULT_FONT_SPINNER_POSITION = 0;
    private static final int DEFAULT_LOCALE_SPINNER_POSITION = 1;

    private LinkedHashMap<String, String> appFontsMap;
    private LinkedHashMap<String, String> appLocalesMap;

    private int currentFontSize;
    private String currentFont;
    private int currentFontPosition;
    private String currentLocale;
    private int currentLocalePosition;

    public int getCurrentFontSize() {
        return currentFontSize;
    }

    //public void setCurrentFontSize(int currentFontSize) { this.currentFontSize = currentFontSize; }

    public String getCurrentFont() { return currentFont; }

    //public void setCurrentFont(String currentFont) { this.currentFont = currentFont; }

    public String getCurrentLocale() { return currentLocale; }

    //public void setCurrentLocale(String currentLocale) { this.currentLocale = currentLocale; }


    public interface SettingsNoticeDialogListener {
        void onSettingsDialogPositiveClick(DialogFragment dialog);
        void onSettingsDialogNegativeClick(DialogFragment dialog);
    }

    private SettingsNoticeDialogListener _listener;

    public  SettingsDialogFragment() {
        currentFontSize = DEFAULT_TEXT_SIZE_SCALE;
        currentFont = "Default";
        currentFontPosition = DEFAULT_FONT_SPINNER_POSITION;
        currentLocale = "ru";
        currentLocalePosition = DEFAULT_LOCALE_SPINNER_POSITION;

        fullAppFontsMap();
        fullAppLocalesMap();
    }

    public SettingsDialogFragment(int fontSize, String font, String locale) {
        this();
        currentFontSize = fontSize;
        currentFont = font;
        currentLocale = locale;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        // Verify that the host activity implements the callback interface
        try {
            // Instantiate the NoticeDialogListener so we can send events to the host
            _listener = (SettingsNoticeDialogListener) activity;
        } catch (ClassCastException e) {
            // The activity doesn't implement the interface, throw exception
            throw new ClassCastException(activity.toString()
                    + " must implement NoticeDialogListener");
        }
    }

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        // Get the layout inflater
        LayoutInflater inflater = getActivity().getLayoutInflater();

        View currentView = inflater.inflate(R.layout.settings_item_constraint, null);

        //seekBar
        final SeekBar seekBar = currentView.findViewById(R.id.seekBar);
        seekBar.setProgress(currentFontSize);
        if (seekBar != null) {
            seekBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
                @Override
                public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                    currentFontSize = progress;
                }

                @Override
                public void onStartTrackingTouch(SeekBar seekBar) {

                }

                @Override
                public void onStopTrackingTouch(SeekBar seekBar) {

                }
            });
        }
        //seekBar


        //Spinner for FONTS
        Spinner fontsSpinner = currentView.findViewById(R.id.app_fonts);
        // Создаем адаптер ArrayAdapter с помощью массива строк и стандартной разметки элемета spinner
        ArrayAdapter<String> fontsAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, getFontsNames());
        // Определяем разметку для использования при выборе элемента
        fontsAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        // Применяем адаптер к элементу spinner
        fontsSpinner.setAdapter(fontsAdapter);

        fontsSpinner.setSelection(currentFontPosition);

        AdapterView.OnItemSelectedListener fontsItemSelectedListener = new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                // Получаем выбранный объект
                currentFont = appFontsMap.get(parent.getItemAtPosition(position));
                currentFontPosition = position;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        };
        fontsSpinner.setOnItemSelectedListener(fontsItemSelectedListener);
        //Spinner for FONTS


        //Spinner for LOCALES
        Spinner localesSpinner = currentView.findViewById(R.id.app_locales);
        // Создаем адаптер ArrayAdapter с помощью массива строк и стандартной разметки элемета spinner
        ArrayAdapter<String> localesAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, getLocalesNames());
        // Определяем разметку для использования при выборе элемента
        localesAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        // Применяем адаптер к элементу spinner
        localesSpinner.setAdapter(localesAdapter);

        localesSpinner.setSelection(currentLocalePosition);

        AdapterView.OnItemSelectedListener localesItemSelectedListener = new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                // Получаем выбранный объект
                currentLocale = appLocalesMap.get(parent.getItemAtPosition(position));
                currentLocalePosition = position;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        };
        localesSpinner.setOnItemSelectedListener(localesItemSelectedListener);
        //Spinner for LOCALES

        // Inflate and set the layout for the dialog
        // Pass null as the parent view because its going in the dialog layout
        builder.setView(currentView)
                // Add action buttons
                .setPositiveButton(R.string.positive_push, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        _listener.onSettingsDialogPositiveClick(SettingsDialogFragment.this);
                    }
                })
                .setNegativeButton(R.string.negative_push, new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        _listener.onSettingsDialogNegativeClick(SettingsDialogFragment.this);
                    }
                });

        return builder.create();
    }


    private void fullAppFontsMap() {
        if (appFontsMap == null) {
            appFontsMap = new LinkedHashMap<>();

            appFontsMap.put("Default", "fonts/Roboto_regular.ttf");
            appFontsMap.put("Liebe finden", "fonts/Liebe_finden.ttf");
            appFontsMap.put("Orange juice", "fonts/orange_juice.ttf");
            appFontsMap.put("Ropest black", "fonts/RopestBlack.ttf");
            appFontsMap.put("Midnight drive", "fonts/Midnight_Drive.otf");
        }
    }

    private ArrayList<String> getFontsNames() {
        ArrayList<String> allFontsNames = new ArrayList<>();
        allFontsNames.addAll(appFontsMap.keySet());
        return allFontsNames;
    }

    private void fullAppLocalesMap() {
        if (appLocalesMap == null) {
            appLocalesMap = new LinkedHashMap<>();

            appLocalesMap.put("English", "en");
            appLocalesMap.put("Русский", "ru");
        }
    }

    private ArrayList<String> getLocalesNames() {
        ArrayList<String> allLocalesNames = new ArrayList<>();
        allLocalesNames.addAll(appLocalesMap.keySet());
        return allLocalesNames;
    }

}

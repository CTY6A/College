package com.stubeda.dips;

import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.preference.Preference;
import androidx.preference.PreferenceFragmentCompat;
import androidx.preference.PreferenceManager;

import java.util.Locale;

public class SettingsActivity extends AppCompatActivity {
    private SettingsFragment curSettingsFragment;
    private SharedPreferences Settings;
    private String curLocale;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);

        if (savedInstanceState == null) {
            getSupportFragmentManager()
                    .beginTransaction()
                    .replace(R.id.ltSettings, curSettingsFragment = new SettingsFragment())
                    .commit();
        }

        Settings = PreferenceManager.getDefaultSharedPreferences(getBaseContext());
        curLocale = Settings.getString("locale", "ru");



        Locale locale = new Locale(Settings.getString("locale", "ru"));
        Locale.setDefault(locale);
        Configuration config = getResources().getConfiguration();
        config.locale = locale;
        getResources().updateConfiguration(config, getResources().getDisplayMetrics());

        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.setDisplayHomeAsUpEnabled(true);
            actionBar.setTitle(R.string.setting);
        }
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.options_menu, menu);
        return true;
    }

    public void onHelpItemClick(MenuItem item) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        LayoutInflater inflater = getLayoutInflater();

        View customView = inflater.inflate(R.layout.help_dialog, null);
        TextView messageView1 = (TextView)customView.findViewById(R.id.txtHelp1);
        TextView messageView2 = (TextView)customView.findViewById(R.id.txtHelp2);
        TextView messageView3 = (TextView)customView.findViewById(R.id.txtHelp3);
        TextView messageView4 = (TextView)customView.findViewById(R.id.txtHelp4);

        builder.setView(customView)
                .setTitle(R.string.help);

        messageView1.setText(R.string.help_setting1);
        messageView2.setText(R.string.help_setting2);
        messageView3.setText(R.string.help_setting3);
        messageView4.setText(R.string.help_setting4);

        AlertDialog alert = builder.create();

        alert.show();
    }

    public boolean onOptionsItemSelected(MenuItem item){
        finish();
        return true;
    }

    public static class SettingsFragment extends PreferenceFragmentCompat {
        @Override
        public void onCreatePreferences(Bundle savedInstanceState, String rootKey) {
            setPreferencesFromResource(R.xml.root_preferences, rootKey);
        }
    }

    @Override
    protected void onResume() {
        super.onResume();

        Preference curPreference = curSettingsFragment.findPreference("locale");

        curPreference.setOnPreferenceChangeListener((preference, newValue) -> {
            if (!curLocale.equals(newValue.toString())) {
                Intent intent = getIntent();
                finish();
                startActivity(intent);
            }

            return true;
        });
    }
}
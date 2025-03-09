package com.stubeda.dips;

import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.preference.PreferenceManager;

import java.util.Locale;

public class MainActivity extends AppCompatActivity {
    private String curLocale;
    private SharedPreferences Settings;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Settings = PreferenceManager.getDefaultSharedPreferences(getBaseContext());
        curLocale = Settings.getString("locale", "ru");

        Locale locale = new Locale(Settings.getString("locale", "ru"));
        Locale.setDefault(locale);
        Configuration config = getBaseContext().getResources().getConfiguration();
        config.locale = locale;
        getBaseContext().getResources().updateConfiguration(config,
                getBaseContext().getResources().getDisplayMetrics());

        setContentView(R.layout.activity_main);

        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.setDisplayHomeAsUpEnabled(true);
            actionBar.setTitle(R.string.simre);
        }
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        finish();
        startActivity(new Intent(this, SplashActivity.class));
        return true;
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
        TextView messageView1 = customView.findViewById(R.id.txtHelp1);
        TextView messageView2 = customView.findViewById(R.id.txtHelp2);
        TextView messageView3 = customView.findViewById(R.id.txtHelp3);
        TextView messageView4 = customView.findViewById(R.id.txtHelp4);

        builder.setView(customView)
                .setTitle(R.string.help);

        messageView1.setText(R.string.help_main1);
        messageView2.setText(R.string.help_main2);
        messageView3.setText(R.string.help_main3);
        messageView4.setText(R.string.help_main4);

        AlertDialog alert = builder.create();

        alert.show();
    }

    public void onClickExit(View view) {
        this.finish();
        System.exit(0);
    }

    public void onClickSetting(View view) {
        this.startActivity(new Intent(this, SettingsActivity.class));
    }

    public void onClickTrain(View view) {
        this.startActivity(new Intent(this, TestingActivity.class));
    }
    public void onClickResults(View view) {
        this.startActivity(new Intent(this, FastStatsActivity.class));
    }

    @Override
    protected void onResume() {
        super.onResume();

        Settings = PreferenceManager.getDefaultSharedPreferences(getBaseContext());

        if (!curLocale.equals(Settings.getString("locale", "ru"))) {
            Intent intent = getIntent();
            finish();
            startActivity(intent);
        }

        Locale locale = new Locale(Settings.getString("locale", "ru"));
        Locale.setDefault(locale);
        Configuration config = getBaseContext().getResources().getConfiguration();
        config.locale = locale;
        getBaseContext().getResources().updateConfiguration(config,
                getBaseContext().getResources().getDisplayMetrics());


    }
}
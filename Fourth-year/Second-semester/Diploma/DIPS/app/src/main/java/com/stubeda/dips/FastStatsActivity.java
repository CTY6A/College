package com.stubeda.dips;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.view.MenuItem;
import android.widget.TextView;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.preference.PreferenceManager;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class FastStatsActivity extends AppCompatActivity {

    private final Activity curActivity = this;
    private final UserStatsGETTask userStatsGETTask = new UserStatsGETTask();
    @SuppressLint("StaticFieldLeak")
    private class UserStatsGETTask extends AsyncTask<String, String, String> {

        String answerHTTP = "";
        JSONArray postDataParams = new JSONArray();

        String serverURL = "http://172.20.10.6:8080/api/v1/users";

        @Override
        protected String doInBackground(String... params) {
            URL url;
            HttpURLConnection urlConnection = null;
            try {
                url = new URL(serverURL);

                urlConnection = (HttpURLConnection) url.openConnection();

                int responseCode = urlConnection.getResponseCode();
                if(responseCode == HttpURLConnection.HTTP_OK){
                    answerHTTP = convertInputStreamToString(urlConnection.getInputStream());
                }
            } catch (Exception e) {
                e.printStackTrace();
            } finally {
                if (urlConnection != null) {
                    urlConnection.disconnect();
                }
            }

            try {
                postDataParams = new JSONArray(answerHTTP);
            } catch (JSONException e) {
                e.printStackTrace();
            }

            return null;
        }

        @Override
        protected void onPostExecute(String result) {
            if (answerHTTP.length() != 0) {
                StringBuilder answer = new StringBuilder();
                for (int i = 0; i < postDataParams.length(); i++) {
                    try {
                        answer
                                .append(getString(R.string.name)).append(": ")
                                .append(((JSONObject) postDataParams.get(i)).getString("name"))
                                .append("\n")

                                .append(getString(R.string.date)).append(": ")
                                .append(((JSONObject) postDataParams.get(i)).getString("date"))
                                .append("\n")

                                .append(getString(R.string.reaction)).append(": ")
                                .append(((JSONObject) postDataParams.get(i)).getString("reaction"))
                                .append("\n")

                                .append(getString(R.string.work_zone)).append(": ")
                                .append(((JSONObject) postDataParams.get(i)).getString("work_zone"))
                                .append("\n")

                                .append(getString(R.string.time)).append(": ")
                                .append("\n")
                                .append(((JSONObject) postDataParams.get(i)).getString("time")
                                        .replaceAll(";", "\n"))
                                .append("\n")
                                .append("\n");
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                }
                textView.setText(answer.toString());
            }
            else {
                AlertDialog.Builder builder = new AlertDialog.Builder(curActivity);
                builder.setTitle("Server answer")
                        .setMessage(R.string.failed_to_connect_to_server)
                        .setPositiveButton(R.string.ok, (dialog, id) -> dialog.cancel());

                AlertDialog alert = builder.create();

                alert.show();
            }

            super.onPostExecute(result);
        }

        private String convertInputStreamToString(InputStream in) {
            BufferedReader reader = null;
            StringBuilder response = new StringBuilder();
            try {
                reader = new BufferedReader(new InputStreamReader(in));
                String line;
                while ((line = reader.readLine()) != null) {
                    response.append(line);
                }
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
                if (reader != null) {
                    try {
                        reader.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
            return response.toString();
        }
    }

    SharedPreferences Settings;

    private String curName = "";
    private String curDate = "";
    private String curReaction = "";
    private String curWorkZone = "";
    private String curTimeOfAttempts = "";

    private TextView textView;

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fast_stats);

        Settings = PreferenceManager.getDefaultSharedPreferences(getBaseContext());

        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.setDisplayHomeAsUpEnabled(true);
            actionBar.setTitle(R.string.fast_stats);
        }
        textView = new TextView(this);
        textView.setTextSize(16);
        textView.setPadding(16, 16, 16, 16);
        textView.setBackgroundColor(Color.parseColor(Settings.getString("background_color", "#004D40")));
        textView.setMovementMethod(new ScrollingMovementMethod());
        textView.setTextColor(Color.WHITE);

        curName = Settings.getString("curName", curName);
        curDate = Settings.getString("curDate", curDate);
        curReaction = Settings.getString("curReaction", curReaction);
        curWorkZone = Settings.getString("curWorkZone", curWorkZone);
        curTimeOfAttempts = Settings.getString("curTimeOfAttempts", curTimeOfAttempts);

        userStatsGETTask.execute();

        textView.setText(
                getString(R.string.name) + ": " + curName + ";\n" +
                getString(R.string.date) + ": " + curDate + ";\n" +
                getString(R.string.reaction) + ": " + curReaction + ";\n" +
                getString(R.string.work_zone) + ": " + curWorkZone + ";\n" +
                getString(R.string.time) + ": " + curTimeOfAttempts
        );

        setContentView(textView);
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        finish();
        return true;
    }
}
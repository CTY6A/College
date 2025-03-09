package com.stubeda.dips;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.SystemClock;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.preference.PreferenceManager;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.Random;

public class TestingActivity extends AppCompatActivity {

    @SuppressLint("StaticFieldLeak")
    private class UserStatsPOSTTask extends AsyncTask<String, String, String> {

        String answerHTTP;
        String name, date, reaction, work_zone, time;
        JSONObject postDataParams;

        String serverURL = "http://172.20.10.6:8080/api/v1/users";

        @Override
        protected void onPreExecute() {
            name = curName;
            date = curDate;
            reaction = curReaction;
            work_zone = curWorkZone;
            time = curTimeOfAttempts;

            super.onPreExecute();
        }

        @Override
        protected String doInBackground(String... params) {
            postDataParams = new JSONObject();

            try {
                postDataParams.put("name", name);
                postDataParams.put("date", date);
                postDataParams.put("reaction", reaction);
                postDataParams.put("work_zone", work_zone);
                postDataParams.put("time", time);
            } catch (JSONException e) {
                e.printStackTrace();
            }

            answerHTTP = performPostCall(serverURL, postDataParams);

            return null;
        }

        @Override
        protected void onPostExecute(String result) {
            AlertDialog.Builder builder = new AlertDialog.Builder(curActivity);
            builder.setTitle(R.string.server_answer)
                    .setMessage(answerHTTP.equals("") ? R.string.data_not_sent_to_server : R.string.data_sent_to_server)
                    .setPositiveButton(R.string.ok, (dialog, id) -> {
                        dialog.cancel();

                        if (Settings.getBoolean("show_statistics", true)) {
                            Intent intent = new Intent(curActivity, FastStatsActivity.class);
                            startActivity(intent);
                        }
                    });

            AlertDialog alert = builder.create();

            alert.show();

            super.onPostExecute(result);
        }

        public String performPostCall(String requestURL,
                                      JSONObject postDataParams) {
            URL url;
            StringBuilder response = new StringBuilder();
            try {
                url = new URL(requestURL);

                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setReadTimeout(200);
                conn.setConnectTimeout(200);
                conn.setRequestMethod("POST");
                conn.setDoInput(true);
                conn.setDoOutput(true);

                conn.addRequestProperty("Content-Type", "application/json");

                OutputStream os = conn.getOutputStream();
                os.write(postDataParams.toString().getBytes());
                os.close();

                int responseCode = conn.getResponseCode();

                if (responseCode == HttpURLConnection.HTTP_OK) {
                    String line;
                    BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                    while ((line = br.readLine()) != null) {
                        response.append(line);
                    }
                } else {
                    response = new StringBuilder();

                }
            } catch (Exception e) {
                e.printStackTrace();
            }

            return response.toString();
        }
    }

    private int curStopwatch = 0;
    private int curCountRight = 0;
    private int curCountFall = 0;
    private int curSecondWaveform;
    private int curFirstWaveform;
    private int curLimitingAttempts;
    private String curName = "";
    private String curDate = "";
    private String curReaction = "";
    private String curWorkZone = "";
    private String curTimeOfAttempts = "";
    private final Random curRandom = new Random();
    private boolean flgTooEarlyMistake = false;
    private boolean flgTooLateMistake = false;
    private boolean flgTestingInProgress = false;
    private boolean flgPreparationForTheTest = false;
    private boolean flgTestInProgress = false;
    private boolean flgPassWithoutError = false;
    private boolean flgReactionClickHold;

    SharedPreferences Settings;

    private View ltLed;
    private Button btnLed;
    private Button btnStartFinish;
    private TextView txtTimer;
    private TextView txtCounterRight;
    private TextView txtCounterFall;

    private final UserStatsPOSTTask userStatsPOSTTask = new UserStatsPOSTTask();
    private Thread Test;
    private final Activity curActivity = this;

    @SuppressLint({"ClickableViewAccessibility", "SimpleDateFormat"})
    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(R.layout.activity_testing);

        ltLed = findViewById(R.id.ltSignal);
        btnLed = findViewById(R.id.btnSignal);
        btnStartFinish = findViewById(R.id.btnStartFinish);
        txtTimer = findViewById(R.id.txtTimer);
        txtCounterRight = findViewById(R.id.txtCounterRight);
        txtCounterFall = findViewById(R.id.txtCounterFall);

        Settings = PreferenceManager.getDefaultSharedPreferences(getBaseContext());
        findViewById(R.id.ltTrain).setBackgroundColor(Color.parseColor(Settings.getString("background_color", "#004D40")));
        flgPassWithoutError = Settings.getBoolean("pass_without_error", false);
        curFirstWaveform = curChooseSignal(Settings.getString("first_signal", "nothing"));
        curSecondWaveform = curChooseSignal(Settings.getString("second_signal", "cyan"));
        if (!Settings.getBoolean("highlight_with_dotted_line", true)){
            if (curFirstWaveform == R.drawable.dotted_border){
                curFirstWaveform = R.drawable.color_icon_transparent;
            }
            if (curSecondWaveform == R.drawable.dotted_border){
                curSecondWaveform = R.drawable.color_icon_transparent;
            }
        }
        if (Settings.getString("workzone", "big").compareTo("big") == 0){
            ViewGroup.LayoutParams params = btnLed.getLayoutParams();
            params.width = params.height = 750;
            btnLed.setLayoutParams(params);
        }
        else {
            ViewGroup.LayoutParams params = btnLed.getLayoutParams();
            params.width = params.height = 325;
            btnLed.setLayoutParams(params);
        }
        flgReactionClickHold = Settings.getString("reaction", "click").compareTo("click") != 0;
        try {
            curLimitingAttempts = Integer.parseInt(Settings.getString("number_of_attempts", "20"));
        }
        catch (Exception e) {
            curLimitingAttempts = 0;
        }
        Log.d("1", String.valueOf(curLimitingAttempts));

        btnLed.setOnTouchListener((v, event) -> {
            if (flgTestingInProgress) {
                if (event.getAction() == MotionEvent.ACTION_DOWN) {
                    if (!flgReactionClickHold) {
                        if (flgTestInProgress) {
                            ltLed.setBackgroundResource(curFirstWaveform);
                            txtCounterRight.setText(String.valueOf(++curCountRight));

                            curTimeOfAttempts += (float) curStopwatch / 1000 + ";";


                        } else {
                            flgTooEarlyMistake = true;

                            txtTimer.setText(R.string.too_early);
                            setCurCountFall();
                        }
                        txtCounterFall.setText(String.valueOf(curCountFall));


                        flgTestInProgress = false;
                    } else {
                        flgPreparationForTheTest = true;
                    }

                } else if (event.getAction() == MotionEvent.ACTION_UP) {
                    if (flgReactionClickHold) {
                        if (flgTestInProgress) {
                            ltLed.setBackgroundResource(curFirstWaveform);
                            txtCounterRight.setText(String.valueOf(++curCountRight));

                            curTimeOfAttempts += (float) curStopwatch / 1000 + ";";

                        } else if (!flgTooLateMistake & flgPreparationForTheTest) {
                            flgTooEarlyMistake = true;

                            txtTimer.setText(R.string.too_early);

                            setCurCountFall();
                            txtCounterFall.setText(String.valueOf(curCountFall));
                        } else flgTooLateMistake = false;
                        flgTestInProgress = false;
                    }
                }
            }
            return false;
        });

        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.setDisplayHomeAsUpEnabled(true);
            if (curLimitingAttempts == 0) {
                actionBar.setTitle(R.string.train);
            }
            else {
                actionBar.setTitle(R.string.test);
                actionBar.setTitle(actionBar.getTitle() + " " + curLimitingAttempts);
            }
        }

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        LayoutInflater inflater = getLayoutInflater();

        View customView = inflater.inflate(R.layout.help_dialog, null);
        TextView messageView1 = customView.findViewById(R.id.txtHelp1);
        TextView messageView2 = customView.findViewById(R.id.txtHelp2);
        TextView messageView3 = customView.findViewById(R.id.txtHelp3);
        TextView messageView4 = customView.findViewById(R.id.txtHelp4);

        builder.setView(customView)
                .setTitle(R.string.help);

        messageView1.setText(R.string.help_testing1);
        messageView2.setText(R.string.help_testing2);
        messageView3.setText(R.string.help_testing3);
        messageView4.setText(R.string.help_testing4);


        AlertDialog alert = builder.create();

        alert.show();

    }

    public void setCurCountFall() {
        curCountFall++;
        curTimeOfAttempts += "-;";
        if (curLimitingAttempts != 0 && curLimitingAttempts / 2 < curCountFall) {
            txtTimer.setText(R.string.try_again_later);

            flgTestInProgress = false;
            flgTestingInProgress = false;

            btnLed.setVisibility(View.INVISIBLE);
            btnStartFinish.setText(R.string.start);
            ltLed.setBackgroundResource(curFirstWaveform);

            try {
                Test.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            btnStartFinish.setEnabled(false);
        }
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.options_menu, menu);
        return true;
    }

    @SuppressLint("InflateParams")
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

        messageView1.setText(R.string.help_testing1);
        messageView2.setText(R.string.help_testing2);
        messageView3.setText(R.string.help_testing3);
        messageView4.setText(R.string.help_testing4);


        AlertDialog alert = builder.create();

        alert.show();
    }

    protected void onResume() {
        super.onResume();


        Locale locale = new Locale(Settings.getString("locale", "ru"));
        Locale.setDefault(locale);
        Configuration config = getBaseContext().getResources().getConfiguration();
        config.locale = locale;
        getBaseContext().getResources().updateConfiguration(config,
                getBaseContext().getResources().getDisplayMetrics());
        if (curLimitingAttempts != 0) {
            txtTimer.setText("");
        }
        else {
            if (curStopwatch != 0) {
                txtTimer.setText(String.valueOf((float) curStopwatch / 1000));
            } else {
                txtTimer.setText(R.string._0_0000);
            }
        }
        txtCounterRight.setText(String.valueOf(curCountRight));
        txtCounterFall.setText(String.valueOf(curCountFall));

        ltLed.setBackgroundResource(curFirstWaveform);

        if (flgTestingInProgress) {
            btnLed.setVisibility(View.VISIBLE);
            btnStartFinish.setText(R.string.finish);

            Test = new Thread(this::curTest);
            Test.start();
        }
    }

    @Override
    protected void onPause() {
        super.onPause();

        Settings.edit().putString("curName", curName).apply();
        Settings.edit().putString("curDate", curDate).apply();
        Settings.edit().putString("curReaction", curReaction).apply();
        Settings.edit().putString("curWorkZone", curWorkZone).apply();
        Settings.edit().putString("curTimeOfAttempts", curTimeOfAttempts).apply();
    }

    public boolean onOptionsItemSelected(MenuItem menuItem) {
        finish();
        return true;
    }

    @SuppressLint("SimpleDateFormat")
    public void onClickStarFinish(View view) throws InterruptedException {
        if (!flgTestingInProgress) {
            curCountRight = 0;
            curCountFall = 0;
            flgTestingInProgress = true;
            if(!flgReactionClickHold) {
                flgPreparationForTheTest = true;
            }

            btnLed.setVisibility(View.VISIBLE);
            btnStartFinish.setText(R.string.finish);

            if (curLimitingAttempts == 0){
                txtTimer.setText(R.string._0_0000);
            }
            else {
                txtTimer.setText("");
            }
            txtCounterRight.setText(R.string._0);
            txtCounterFall.setText(R.string._0);

            curDate = new SimpleDateFormat("yyyy.MM.dd HH:mm:ss").format(new Date(System.currentTimeMillis()));
            curReaction = (flgReactionClickHold ? getString(R.string.hold) : getString(R.string.click));
            curWorkZone = (Settings.getString("workzone", "big").compareTo("big") == 0 ? getString(R.string.big) : getString(R.string.small));

            Test = new Thread(this::curTest);
            Test.start();
        }
        else {
            flgTestInProgress = false;
            flgTestingInProgress = false;

            btnLed.setVisibility(View.INVISIBLE);
            btnStartFinish.setText(R.string.start);
            ltLed.setBackgroundResource(curFirstWaveform);

            Test.join();
        }
    }

    protected void onSaveInstanceState(@NonNull Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putInt("curStopwatch", curStopwatch);
        outState.putInt("curCountRight", curCountRight);
        outState.putInt("curCountFall", curCountFall);

        outState.putBoolean("flgTestingInProgress", flgTestingInProgress);
        outState.putBoolean("flgTestInProgress", flgTestInProgress);

        outState.putString("curName", curName);
        outState.putString("curDate", curDate);
        outState.putString("curReaction", curReaction);
        outState.putString("curWorkZone", curWorkZone);
        outState.putString("curTimeOfAttempts", curTimeOfAttempts);
    }

    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
        curStopwatch = savedInstanceState.getInt("curStopwatch");
        curCountRight = savedInstanceState.getInt("curCountRight");
        curCountFall = savedInstanceState.getInt("curCountFall");

        flgTestingInProgress = savedInstanceState.getBoolean("flgTestingInProgress");
        flgTestInProgress = savedInstanceState.getBoolean("flgTestInProgress");

        curName = savedInstanceState.getString("curName", curName);
        curDate = savedInstanceState.getString("curDate", curDate);
        curReaction = savedInstanceState.getString("curReaction", curReaction);
        curWorkZone = savedInstanceState.getString("curWorkZone", curWorkZone);
        curTimeOfAttempts = savedInstanceState.getString("curTimeOfAttempts", curTimeOfAttempts);
    }

    public int curChooseSignal(String curCondition) {
        switch (curCondition) {
            case "serios_smile":
                return R.drawable.pic1;

            case "smile_with_tongue":
                return R.drawable.pic2;

            case "fencer_number_one":
                return R.drawable.fenc_1;

            case "fencer_number_two":
                return R.drawable.fenc_2;

            case "black":
                return R.drawable.color_icon_black;

            case "red":
                return R.drawable.color_icon_red;

            case "yellow":
                return R.drawable.color_icon_yellow;

            case "gray":
                return R.drawable.color_icon_gray;

            case "#004D40":
                return R.drawable.color_icon_green;

            case "cyan":
                return R.drawable.color_icon_cyan;

            default:
                return R.drawable.dotted_border;
        }
    }

    @SuppressLint("InflateParams")
    public void curTest() {
        while (flgTestingInProgress & ((curLimitingAttempts != 0 & curCountRight < curLimitingAttempts) | (curLimitingAttempts == 0))) {
            if (flgPreparationForTheTest) {
                if (!flgTestInProgress) {
                    int curTimer = curRandom.nextInt(2000) + 1000;
                    while (flgTestingInProgress & curTimer-- > 0 & !flgTooEarlyMistake) {
                        try {
                            Thread.sleep(1);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }

                    curStopwatch = 0;
                    if (flgTestingInProgress & !flgTooEarlyMistake) {
                        flgTestInProgress = true;
                        runOnUiThread(() -> ltLed.setBackgroundResource(curSecondWaveform));
                    }
                } else {
                    runOnUiThread(() -> ltLed.setBackgroundResource(curSecondWaveform));
                }

                if (flgPreparationForTheTest) {
                    if (flgReactionClickHold) {
                        flgPreparationForTheTest = false;
                    }
                    if (flgTestingInProgress & flgTestInProgress & !flgTooEarlyMistake){
                        runOnUiThread(() -> txtTimer.setText(""));
                    }
                    while (flgTestingInProgress & flgTestInProgress & curStopwatch < 500 & !flgTooEarlyMistake) {
                        try {
                            Thread.sleep(1);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                        if (flgTestingInProgress & flgTestInProgress & !flgTooEarlyMistake) {
                            curStopwatch++;
                            if (curLimitingAttempts == 0) {
                                runOnUiThread(() -> txtTimer.setText(String.valueOf((float) curStopwatch / 1000)));
                            }
                        }
                    }
                }

                if (curStopwatch == 500) {
                    flgTestInProgress = false;
                    flgTooLateMistake = true;
                    runOnUiThread(() -> {
                        if (!flgPassWithoutError) {
                            txtTimer.setText(R.string.too_late);

                            setCurCountFall();
                            txtCounterFall.setText(String.valueOf(curCountFall));
                        }
                        ltLed.setBackgroundResource(curFirstWaveform);
                        btnLed.dispatchTouchEvent(MotionEvent.obtain(SystemClock.uptimeMillis(), SystemClock.uptimeMillis(), MotionEvent.ACTION_UP,  0, 0, 0));
                    });
                }

                if (flgTooEarlyMistake) {
                    flgTooEarlyMistake = false;
                }
            }
            else {
                try {
                    Thread.sleep(1);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
        if (curLimitingAttempts != 0 & curCountRight == curLimitingAttempts) {
            runOnUiThread(() -> {
                btnLed.setVisibility(View.INVISIBLE);
                btnStartFinish.setText(R.string.start);
                ltLed.setBackgroundResource(curFirstWaveform);


                AlertDialog.Builder builder = new AlertDialog.Builder(this);

                final EditText input = new EditText(TestingActivity.this);
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(
                        LinearLayout.LayoutParams.MATCH_PARENT,
                        LinearLayout.LayoutParams.MATCH_PARENT);
                input.setLayoutParams(lp);
                input.setHint(R.string.name);
                builder.setView(input)
                        .setTitle(R.string.enter_your_name)
                        .setPositiveButton(R.string.ok, (dialog, id) -> {

                            curName = input.getText().toString();

                            userStatsPOSTTask.execute();


                        })
                        .setNegativeButton(R.string.cancel, (dialog, id) -> dialog.cancel());

                AlertDialog alert = builder.create();

                alert.show();
            });
            flgTestInProgress = false;
            flgTestingInProgress = false;
        }
    }
}
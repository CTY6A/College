package com.example.kinopoisk_10;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;

import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.io.BufferedInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.text.DecimalFormat;
import java.util.ArrayList;

public class FilmAdapter extends RecyclerView.Adapter<FilmViewHolder> {

    /*
    public static final String APP_IMAGES_PATH =
            Environment.getExternalStorageDirectory().getAbsolutePath() + "/Kinopoisk/" + "/Images/";
     */

    private static final String POSTER_URI_START = "https://avatars.mds.yandex.net/get-kinopoisk-image/";

    //Здесь мы будем хранить набор наших данных
    private ArrayList<Film> _films;
    private Context _context;
    public static OnItemClickListener listener;

    public FilmAdapter(Context context, ArrayList<Film> films){
        _context = context;
        _films = new ArrayList<Film>(films);
    }


    //Этот метод вызывается при создании нового ViewHolder'а
    @Override
    public FilmViewHolder onCreateViewHolder(ViewGroup viewGroup, int i){
        View itemView = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.list_item_constraint, viewGroup, false);
        return new FilmViewHolder(itemView);
    }


    //Этот метод вызывается при прикреплении нового элемента к RecyclerView
    @Override
    public void onBindViewHolder(FilmViewHolder filmViewHolder, int position){
        //Получаем элемент набора данных для заполнения
        Film currentFilm = _films.get(position);

        DecimalFormat decimalFormat = new DecimalFormat("#.##");
        double filmRating = (double)currentFilm.getRatingsSum() / currentFilm.getViews();

        if (filmRating > 10)
            filmRating = 10;

        String genres = "";
        for (int i = 0; i < currentFilm.getGenres().size(); i++) {
            genres += currentFilm.getGenres().get(i);
            if (i != currentFilm.getGenres().size() - 1)
                genres += ", ";
        }

        //Заполняем поля viewHolder'а данными из элемента набора данных
        filmViewHolder.tvGenres.setText(genres);
        filmViewHolder.tvName.setText(currentFilm.getName());
        filmViewHolder.tvRating.setText(decimalFormat.format(filmRating));
        filmViewHolder.tvViews.setText(String.valueOf(currentFilm.getViews()));

        Glide.with(_context).load(AllConstants.POSTER_URI_START + currentFilm.getPosterURI()).into(filmViewHolder.filmImage);

        /*
        mImageView.setImageBitmap( imageUtil.getImageBitmap() );

        FileInputStream fis = new FileInputStream(Constants.APP_CACHE_PATH + this.image);
        BufferedInputStream bis = new BufferedInputStream(fis);

        Bitmap img = BitmapFactory.decodeStream(bis);
        */
    }


    //Этот метод возвращает количество элементов списка
    @Override
    public int getItemCount(){
        return _films.size();
    }

    public interface OnItemClickListener{
        void onItemClick(View itemView, int position);
    }

    public void setOnItemClickListener(OnItemClickListener listener){
        FilmAdapter.listener = listener;
    }
}

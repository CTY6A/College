package com.example.kinopoisk_10;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

public class FilmViewHolder extends RecyclerView.ViewHolder {
    //объявим поля, созданные в файле интерфейса list_tem.xml
    public TextView tvGenres;
    public TextView tvName;
    public TextView tvRating;
    public TextView tvViews;
    public ImageView filmImage;

    public FilmViewHolder(final View itemView){
        super(itemView);
        //привязываем элементы к полям
        tvGenres = itemView.findViewById(R.id.genres_text_view);
        tvName = itemView.findViewById(R.id.name_text_view);
        tvRating = itemView.findViewById(R.id.rating_text_view);
        tvViews = itemView.findViewById(R.id.views_text_view);
        filmImage = itemView.findViewById(R.id.film_image);

        itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (FilmAdapter.listener != null){
                    FilmAdapter.listener.onItemClick(itemView, getLayoutPosition());
                }
            }
        });
    }
}

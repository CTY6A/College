package com.example.kinopoisk_10;

import io.realm.RealmObject;

public class FilmsVersion extends RealmObject {

    private long version;

    public long getVersion() { return version; }

    public void setVersion(long version) { this.version = version; }

    public FilmsVersion() { }

    public FilmsVersion(long version) { this.version = version; }
}

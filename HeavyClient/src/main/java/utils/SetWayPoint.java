/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package main.java.utils;

import org.jxmapviewer.viewer.DefaultWaypoint;
import org.jxmapviewer.viewer.GeoPosition;

import java.util.Objects;

/**
 *
 * @author Badr
 */
public class SetWayPoint extends DefaultWaypoint{

    public SetWayPoint() {
    }

    public SetWayPoint(double latitude, double longitude) {
        super(latitude, longitude);
    }

    public SetWayPoint(GeoPosition coord) {
        super(coord);
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof SetWayPoint)) return false;
        SetWayPoint that = (SetWayPoint) o;
        return getPosition().getLongitude() == that.getPosition().getLongitude() && getPosition().getLatitude() == that.getPosition().getLatitude();
    }

    @Override
    public int hashCode() {
        return Objects.hash(getPosition().getLongitude(), getPosition().getLatitude());
    }
}

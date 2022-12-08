//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by FernFlower decompiler)
//

package main.java.listeners;

import java.awt.Rectangle;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.geom.Point2D;
import java.util.HashSet;
import java.util.Set;
import javax.swing.SwingUtilities;
import org.jxmapviewer.JXMapViewer;
import org.jxmapviewer.viewer.DefaultWaypoint;
import org.jxmapviewer.viewer.GeoPosition;
import org.jxmapviewer.viewer.Waypoint;
import org.jxmapviewer.viewer.WaypointPainter;

public class MousePositionSelectionListener extends MouseAdapter {
    private JXMapViewer viewer;

    public MousePositionSelectionListener(JXMapViewer viewer) {
        this.viewer = viewer;
    }

    public void mousePressed(MouseEvent evt) {
        boolean left = SwingUtilities.isLeftMouseButton(evt);
        if (left){
            selectPosition(evt);
        }
    }

    private void selectPosition(MouseEvent evt) {
        WaypointPainter<Waypoint> waypointPainter = (WaypointPainter<Waypoint>)this.viewer.getOverlayPainter();
        Set<Waypoint> waypoints = waypointPainter.getWaypoints();
        Set<Waypoint> waypointUpdated = new HashSet<Waypoint>(waypoints);
        if(waypoints.isEmpty() || ( waypoints.size() == 1 && !waypoints.contains(convertPointToWaypoint(evt)))){
            waypointUpdated.add(convertPointToWaypoint(evt));
            waypointPainter.setWaypoints(waypointUpdated);
        }
        viewer.setOverlayPainter(waypointPainter);
    }


    private Waypoint convertPointToWaypoint(MouseEvent evt){
        GeoPosition point = this.viewer.convertPointToGeoPosition(evt.getPoint());
        System.out.println("Position" + point.toString());
        return new DefaultWaypoint(point);
    }
}

/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package main.java;

import main.java.listeners.MousePositionSelectionListener;
import java.io.File;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import javax.swing.event.MouseInputListener;
import org.jxmapviewer.JXMapViewer;
import org.jxmapviewer.OSMTileFactoryInfo;
import org.jxmapviewer.cache.FileBasedLocalCache;
import org.jxmapviewer.input.CenterMapListener;
import org.jxmapviewer.input.PanMouseInputListener;
import org.jxmapviewer.input.ZoomMouseWheelListenerCenter;
import org.jxmapviewer.viewer.DefaultTileFactory;
import org.jxmapviewer.viewer.DefaultWaypoint;
import org.jxmapviewer.viewer.GeoPosition;
import org.jxmapviewer.viewer.TileFactoryInfo;
import org.jxmapviewer.viewer.Waypoint;
import org.jxmapviewer.viewer.WaypointPainter;
import main.java.utils.RoutePainter;

/**
 *
 * @author Badr
 */
public class Map extends JXMapViewer{

    public void init(){
        // Create a TileFactoryInfo for OSM
        TileFactoryInfo info = new OSMTileFactoryInfo();
        DefaultTileFactory tileFactory = new DefaultTileFactory(info);
        tileFactory.setThreadPoolSize(8);

        // Setup local file cache
        File cacheDir = new File(System.getProperty("user.home") + File.separator + ".jxmapviewer2");
        tileFactory.setLocalCache(new FileBasedLocalCache(cacheDir, false));

        // Setup JXMapViewer
        this.setTileFactory(tileFactory);

        GeoPosition paris = new GeoPosition(48.8566, 2.3522);
        this.setZoom(5);
        this.setAddressLocation(paris);

        // Add interactions
        MouseInputListener mia = new PanMouseInputListener(this);
        this.addMouseListener(mia);
        this.addMouseMotionListener(mia);
        this.addMouseListener(new CenterMapListener(this));
        this.addMouseWheelListener(new ZoomMouseWheelListenerCenter(this));

        // setting up the waypoints painter
        Set<Waypoint> waypoints = new HashSet<Waypoint>();
        WaypointPainter<Waypoint> waypointPainter = new WaypointPainter<Waypoint>();
        waypointPainter.setWaypoints(waypoints);
        this.setOverlayPainter(waypointPainter);
    }
    
    public void drawRoute(List<GeoPosition> waypoints){
        main.java.utils.RoutePainter routePainter = new RoutePainter(waypoints);
        this.zoomToBestFit(new HashSet<GeoPosition>(waypoints), 0.7);
        this.setOverlayPainter(routePainter);

    }
    
    public void reset(){
        Set<Waypoint> waypoints = new HashSet<Waypoint>();
        WaypointPainter<Waypoint> waypointPainter = new WaypointPainter<Waypoint>();
        waypointPainter.setWaypoints(waypoints);
        this.setOverlayPainter(waypointPainter);
    }
}

package main.java;

import com.google.maps.internal.PolylineEncoding;
import com.google.maps.model.LatLng;
import org.jxmapviewer.viewer.GeoPosition;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import main.java.soap.ws.client.*;



public class SoapClient {
    static final RoutingService service = new RoutingService();
    static final IRoutingService proxy = service.getBasicHttpBindingIRoutingService();

    private StepsSubscriber subscriber;
    private String queueName;
    private Double[] currentLocation;
    private String mode;
    private String msg = "";
    
    private ArrayOfdouble bbox;
   

    public SoapClient(String name, String queueName,Home home){
        this.queueName = queueName;
        this.currentLocation = new Double[2];
        subscriber = new StepsSubscriber(name, queueName,currentLocation, home);
    }

    public List<GeoPosition> getRoute(String origin, String destination) {
        Itinerary itinerary = proxy.getItinerary(origin,destination);
        List<GeoPosition> waypoints = new ArrayList<>();
        
        try{
            msg = itinerary.getMessage().getValue();
            String polylineEncoded = itinerary.getRoutes().getValue().getRoute().get(0).getGeometry().getValue();
            List<LatLng> polyLineCoordinates = PolylineEncoding.decode(polylineEncoded);
        for(int i = 0 ; i < polyLineCoordinates.size() ; i++){
            LatLng coordinate = polyLineCoordinates.get(i);
            GeoPosition dwp = new GeoPosition(coordinate.lat,coordinate.lng);
            waypoints.add(dwp);
        }

        }catch(Exception ex){}
        return waypoints;
    }

    public List<GeoPosition> getRouteV2(String origin, String destination) {
        Itinerary itinerary = proxy.getItineraryV2(origin,destination,queueName);
        List<GeoPosition> waypoints = new ArrayList<>();
        
        bbox = itinerary.getBbox().getValue();
        mode = itinerary.getMode().getValue();
        
        try{
            msg = itinerary.getMessage().getValue();
            String polylineEncoded = itinerary.getRoutes().getValue().getRoute().get(0).getGeometry().getValue();
            List<LatLng> polyLineCoordinates = PolylineEncoding.decode(polylineEncoded);
            for(int i = 0 ; i < polyLineCoordinates.size() ; i++){
                LatLng coordinate = polyLineCoordinates.get(i);
                GeoPosition dwp = new GeoPosition(coordinate.lat,coordinate.lng);
                waypoints.add(dwp);
            }

        }catch(Exception ex){
            System.out.println("ERROR" + ex.getMessage());
        }
        
        System.out.println("Waypoints" + waypoints.size());
        
        return waypoints;
    }

    

    public List<GeoPosition> getNextRoute(){
        List<GeoPosition> waypoints = new ArrayList<>();
        try{
            GeoCoordinate originGeoCoordinates = new GeoCoordinate();
            originGeoCoordinates.setLongitude(currentLocation[1]);
            originGeoCoordinates.setLatitude(currentLocation[0]);
            
            GeoCoordinate destinationGeoCoordinates = new GeoCoordinate();
            destinationGeoCoordinates.setLongitude(bbox.getDouble().get(2));
            destinationGeoCoordinates.setLatitude(bbox.getDouble().get(3));

            
            QueueWayPoints queueWayPoints = proxy.updateSteps(originGeoCoordinates,destinationGeoCoordinates,mode,queueName);
            String polylineEncoded = queueWayPoints.getGeometry().getValue();
            List<LatLng> polyLineCoordinates = PolylineEncoding.decode(polylineEncoded);
            for(int i = 0 ; i < polyLineCoordinates.size() ; i++){
                LatLng coordinate = polyLineCoordinates.get(i);
                GeoPosition dwp = new GeoPosition(coordinate.lat,coordinate.lng);
                waypoints.add(dwp);
            }
        }catch(Exception ex){
            System.out.println("Error " + ex.getMessage());
            ex.printStackTrace();
        }
            return waypoints;
        }

    public String getMode() {
        return mode;
    }

    public void setMode(String mode) {
        this.mode = mode;
    }

    public String getMsg() {
        return msg;
    }

    public void setMsg(String msg) {
        this.msg = msg;
    }
    
}

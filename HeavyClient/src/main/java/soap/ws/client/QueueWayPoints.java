
package main.java.soap.ws.client;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour QueueWayPoints complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="QueueWayPoints"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="currentMode" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="found" type="{http://www.w3.org/2001/XMLSchema}boolean" minOccurs="0"/&gt;
 *         &lt;element name="geometry" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="message" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="queuename" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "QueueWayPoints", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer.Contracts", propOrder = {
    "currentMode",
    "found",
    "geometry",
    "message",
    "queuename"
})
public class QueueWayPoints {

    @XmlElementRef(name = "currentMode", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer.Contracts", type = JAXBElement.class, required = false)
    protected JAXBElement<String> currentMode;
    protected Boolean found;
    @XmlElementRef(name = "geometry", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer.Contracts", type = JAXBElement.class, required = false)
    protected JAXBElement<String> geometry;
    @XmlElementRef(name = "message", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer.Contracts", type = JAXBElement.class, required = false)
    protected JAXBElement<String> message;
    @XmlElementRef(name = "queuename", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer.Contracts", type = JAXBElement.class, required = false)
    protected JAXBElement<String> queuename;

    /**
     * Obtient la valeur de la propriété currentMode.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getCurrentMode() {
        return currentMode;
    }

    /**
     * Définit la valeur de la propriété currentMode.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setCurrentMode(JAXBElement<String> value) {
        this.currentMode = value;
    }

    /**
     * Obtient la valeur de la propriété found.
     * 
     * @return
     *     possible object is
     *     {@link Boolean }
     *     
     */
    public Boolean isFound() {
        return found;
    }

    /**
     * Définit la valeur de la propriété found.
     * 
     * @param value
     *     allowed object is
     *     {@link Boolean }
     *     
     */
    public void setFound(Boolean value) {
        this.found = value;
    }

    /**
     * Obtient la valeur de la propriété geometry.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getGeometry() {
        return geometry;
    }

    /**
     * Définit la valeur de la propriété geometry.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setGeometry(JAXBElement<String> value) {
        this.geometry = value;
    }

    /**
     * Obtient la valeur de la propriété message.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getMessage() {
        return message;
    }

    /**
     * Définit la valeur de la propriété message.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setMessage(JAXBElement<String> value) {
        this.message = value;
    }

    /**
     * Obtient la valeur de la propriété queuename.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getQueuename() {
        return queuename;
    }

    /**
     * Définit la valeur de la propriété queuename.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setQueuename(JAXBElement<String> value) {
        this.queuename = value;
    }

}

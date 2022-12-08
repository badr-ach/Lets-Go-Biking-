
package main.java.soap.ws.client;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour anonymous complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="UpdateStepsResult" type="{http://schemas.datacontract.org/2004/07/RoutingServer.Contracts}QueueWayPoints" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "updateStepsResult"
})
@XmlRootElement(name = "UpdateStepsResponse", namespace = "http://tempuri.org/")
public class UpdateStepsResponse {

    @XmlElementRef(name = "UpdateStepsResult", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<QueueWayPoints> updateStepsResult;

    /**
     * Obtient la valeur de la propriété updateStepsResult.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link QueueWayPoints }{@code >}
     *     
     */
    public JAXBElement<QueueWayPoints> getUpdateStepsResult() {
        return updateStepsResult;
    }

    /**
     * Définit la valeur de la propriété updateStepsResult.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link QueueWayPoints }{@code >}
     *     
     */
    public void setUpdateStepsResult(JAXBElement<QueueWayPoints> value) {
        this.updateStepsResult = value;
    }

}

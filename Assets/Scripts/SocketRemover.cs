using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketRemover : MonoBehaviour
{
  //  public XRSocketInteractor socket; // Assign this in the Inspector
  private XRSocketInteractor socket;
  private XRGrabInteractable grabInteractable;
  private HVRGrabbable hvrGrabbable;
  private Coroutine reAddLayerCoroutine;
  private InteractionLayerMask defaultInteractionLayerMask;
  private void Start()
  {
      grabInteractable = GetComponent<XRGrabInteractable>();
      hvrGrabbable = GetComponent<HVRGrabbable>();
      hvrGrabbable.Grabbed.AddListener(RemoveObjectFromSocket);
      defaultInteractionLayerMask = grabInteractable.interactionLayers;
  }
  public void SetSocket(XRSocketInteractor interactor) => socket = interactor;
  // private Coroutine
    private void RemoveObjectFromSocket(HVRGrabberBase grabberBase, HVRGrabbable grabbable)
    {
        if(socket == null)return;
        if (socket.hasSelection)
        {
            var interactable = socket.firstInteractableSelected;
            socket.interactionManager.SelectExit(socket, interactable);
            if (reAddLayerCoroutine != null)
            {
                StopCoroutine(reAddLayerCoroutine);
            }
            reAddLayerCoroutine = StartCoroutine(ReAddLayerCoroutine());
            Debug.Log("Grabbed");
        }
    }

    private IEnumerator ReAddLayerCoroutine()
    {
        RemoveInteractivity();
        yield return new WaitForSeconds(1f);
        AddInteractivity();
    }
    /// <summary>
    /// Allows it to be insertable again
    /// </summary>
    private void AddInteractivity() => grabInteractable.interactionLayers = defaultInteractionLayerMask;
    /// <summary>
    /// Allows it to be removable by the hands
    /// </summary>
    private void RemoveInteractivity() =>grabInteractable.interactionLayers = 0;
}

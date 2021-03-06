﻿using UnityEngine;
using System.Collections;

public class MenuKeyInput : MonoBehaviour {

    MainMenuHandler mmh;

    bool up, down, action, leftMouse = false;

    

    void Start() {
        mmh = GetComponent<MainMenuHandler>();
    }

    void GetKeyStates() {
        bool upArrow = Input.GetKeyDown(KeyCode.UpArrow);
        bool downArrow = Input.GetKeyDown(KeyCode.DownArrow);
        bool wKey = Input.GetKeyDown(KeyCode.W);
        bool s = Input.GetKeyDown(KeyCode.S);
        bool enter = Input.GetKeyDown(KeyCode.Return);
        bool space = Input.GetKeyDown(KeyCode.Space);

        if (!mmh.InsideMenu) {
            // So that the player can jump in the config.
            space = false;
        }

        if (wKey && s) {
            wKey = s = false;
        }

        if (upArrow && downArrow) {
            upArrow = downArrow = false;
        }

        up = down = action = false;

        if (upArrow || wKey) {
            up = true;
        } else if (s || downArrow) {
            down = true;
        }

        if (space || enter) {
            action = true;
        }

    }

    void SendKeyStates() {
        if (mmh.InsideMenu) {
            if (up) {
                mmh.MoveUp();
            } else if (down) {
                mmh.MoveDown();
            }
        }

        if (action) {
            mmh.PressActionButton();
        }
    }

    void ProcessMouse() {
        if (mmh.InsideMenu) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool didRayHit = Physics.Raycast(ray, out hit);

            if (didRayHit && hit.collider != null) {
                int children = hit.collider.transform.childCount;
                if(children > 1)
                    mmh.SetActiveButton(hit.collider.transform.GetChild(0).gameObject);
            }
        }
        leftMouse = false;
        if (Input.GetMouseButtonDown(0)) {
            leftMouse = true;
        }
    }

    void FixedUpdate() {
        if (leftMouse) {
            mmh.PressActionButton();
        }

        ProcessMouse();
        GetKeyStates();

        SendKeyStates();
    }
}

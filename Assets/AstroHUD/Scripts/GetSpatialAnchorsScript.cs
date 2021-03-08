// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Microsoft.Azure.SpatialAnchors.Unity.Examples
{
    public class GetSpatialAnchorsScript : SpatialAnchorsUtil
    {
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any
        /// of the Update methods are called the first time.
        /// </summary>
        public async override void Start()
        {
            Debug.Log(">>Get Spatial Anchors Script Start");

            base.Start();

            if (!SanityCheckAccessConfiguration())
            {
                return;
            }
            // feedbackBox.text = stateParams[currentAppState].StepMessage;

            Debug.Log("Get Spatial Anchors script started");

            // create session
            Debug.Log("Creating Spatial Anchors session");
            if (CloudManager.Session == null)
            {
                await CloudManager.CreateSessionAsync();
            }
            currentCloudAnchor = null;
            Debug.Log("Spatial Anchors session created");

            // configure session
            Debug.Log("Configuring Spatial Anchors session");
            ConfigureSession();
            Debug.Log("Spatial Anchors session configured");

            // start session
            Debug.Log("Starting Spatial Anchors session");
            await CloudManager.StartSessionAsync();
            Debug.Log("Spatial Anchors session started");

            // create watcher
            Debug.Log("Creating watcher");
            if (currentWatcher != null)
            {
                currentWatcher.Stop();
                currentWatcher = null;
            }
            currentWatcher = CreateWatcher();
            if (currentWatcher == null)
            {
                Debug.Log("Either cloudmanager or session is null, should not be here!");
                // feedbackBox.text = "YIKES - couldn't create watcher!";
                // currentAppState = AppState.DemoStepLookForAnchor;
            } else
            {
                Debug.Log("Watcher created");
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            // stop session
            CloudManager.StopSession();
            currentWatcher = null;

            // complete cleanup
            currentCloudAnchor = null;
            CleanupSpawnedObjects();
        }

        protected override void OnCloudAnchorLocated(AnchorLocatedEventArgs args)
        {
            base.OnCloudAnchorLocated(args);

            if (args.Status == LocateAnchorStatus.Located)
            {
                currentCloudAnchor = args.Anchor;

                UnityDispatcher.InvokeOnAppThread(() =>
                {
                    Pose anchorPose = Pose.identity;

#if UNITY_ANDROID || UNITY_IOS
                    anchorPose = currentCloudAnchor.G
                    etPose();
#endif
                    // HoloLens: The position will be set based on the unityARUserAnchor that was located.
                    // SpawnOrMoveCurrentAnchoredObject(anchorPose.position, anchorPose.rotation);
                    // currentAppState = AppState.DemoStepDeleteFoundAnchor;
                });
            }
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        private void ConfigureSession()
        {
            List<string> anchorsToFind = new List<string>();
            // FIXME: re-enable for query
            //if (currentAppState == AppState.DemoStepCreateSessionForQuery)
            //{
            //    anchorsToFind.Add(currentAnchorId);
            //}

            SetAnchorIdsToLocate(anchorsToFind);
        }
    }
}

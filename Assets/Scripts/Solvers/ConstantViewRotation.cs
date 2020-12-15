// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Utilities.Solvers
{
    /// <summary>
    /// ConstantViewSize solver scales to maintain a constant size relative to the view (currently tied to the Camera)
    /// </summary>
    [AddComponentMenu("Scripts/ConstantViewRotation")]
    public class ConstantViewRotation : Solver
    {
        #region ConstantViewRotation Parameters

        [SerializeField]
        [Tooltip("Which direction to position the element relative to: HeadOriented rolls with the head, HeadFacingWorldUp view direction but ignores head roll, and HeadMoveDirection uses the direction the head last moved without roll")]
        private RadialViewReferenceDirection referenceDirection = RadialViewReferenceDirection.FacingWorldUp;

        /// <summary>
        /// Which direction to position the element relative to:
        /// HeadOriented rolls with the head,
        /// HeadFacingWorldUp view direction but ignores head roll,
        /// and HeadMoveDirection uses the direction the head last moved without roll.
        /// </summary>
        public RadialViewReferenceDirection ReferenceDirection
        {
            get => referenceDirection;
            set => referenceDirection = value;
        }

        /// <summary>
        /// Position to the view direction, or the movement direction, or the direction of the view cone.
        /// </summary>
        private Vector3 SolverReferenceDirection => SolverHandler.TransformTarget != null ? SolverHandler.TransformTarget.forward : Vector3.forward;

        /// <summary>
        /// The up direction to use for orientation.
        /// </summary>
        /// <remarks>Cone may roll with head, or not.</remarks>
        private Vector3 UpReference
        {
            get
            {
                Vector3 upReference = Vector3.up;

                if (referenceDirection == RadialViewReferenceDirection.ObjectOriented)
                {
                    upReference = SolverHandler.TransformTarget != null ? SolverHandler.TransformTarget.up : Vector3.up;
                }

                return upReference;
            }
        }

        private Vector3 ReferencePoint => SolverHandler.TransformTarget != null ? SolverHandler.TransformTarget.position : Vector3.zero;

        #endregion

        protected override void Start()
        {
            base.Start();
        }

        /// <inheritdoc />
        public override void SolverUpdate()
        {
            // Element orientation
            Vector3 refDirUp = UpReference;
            Quaternion goalRotation = Quaternion.LookRotation(SolverReferenceDirection, refDirUp); ;

            // If gravity aligned then zero out the x and z axes on the rotation
            if (referenceDirection == RadialViewReferenceDirection.GravityAligned)
            {
                goalRotation.x = goalRotation.z = 0f;
            }

            GoalRotation = goalRotation;
        }
    }
}

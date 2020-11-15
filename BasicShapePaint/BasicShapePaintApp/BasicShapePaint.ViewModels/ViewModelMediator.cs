namespace BasicShapePaint.ViewModels
{
    using BasicShapePaint.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using static BasicShapePaint.ViewModels.Utilities.MiscellaneousUtilities;

    public partial class BaseViewModel
    {
        #region Protected Classes

        protected static class ViewModelMediator
        {
            internal static CanvasViewModel canvasVM;
            internal static MenuBarViewModel menubarVM;

            #region Private Fields

            private static ReadOnlyDictionary<ViewModelEvent, Type> eventOwners =
                new ReadOnlyDictionary<ViewModelEvent, Type>(new Dictionary<ViewModelEvent, Type>()
                {{ViewModelEvent.DrawingEnded, typeof(CanvasViewModel)},
                {ViewModelEvent.DrawingStarted, typeof(CanvasViewModel)},
                {ViewModelEvent.MovingModeChanged, typeof(MenuBarViewModel)},
                {ViewModelEvent.SelectedShapeChanged, typeof(MenuBarViewModel)},
                });

            private static ReadOnlyDictionary<ViewModelEvent, List<EmptyEventHandler>> eventSubscribers =
                new ReadOnlyDictionary<ViewModelEvent, List<EmptyEventHandler>>(new Dictionary<ViewModelEvent, List<EmptyEventHandler>>()
                {{ViewModelEvent.DrawingEnded, new List<EmptyEventHandler>()},
                {ViewModelEvent.DrawingStarted, new List<EmptyEventHandler>()},
                {ViewModelEvent.MovingModeChanged, new List<EmptyEventHandler>()},
                {ViewModelEvent.SelectedShapeChanged, new List<EmptyEventHandler>()},
                });

            #endregion Private Fields

            static ViewModelMediator()
            {
#if DEBUG
                if (eventOwners.Count != Enum.GetNames(typeof(ViewModelEvent)).Length
                    || eventSubscribers.Count != Enum.GetNames(typeof(ViewModelEvent)).Length)
                {
                    throw new Exception("Total number of View model events do not match the " +
                        "total number of owners and/or subscribers in dictionary.");
                }
#endif
            }

            #region Public Enums

            public enum ViewModelEvent
            {
                DrawingStarted,
                DrawingEnded,
                MovingModeChanged,
                SelectedShapeChanged
            }

            #endregion Public Enums

            #region Public Properties

            public static ShapeType SelectedShapeType { get => menubarVM.SelectedShapeType; }

            public static Brush SelectedColor { get => menubarVM.SelectedColor; }

            public static bool MovingMode { get => menubarVM.MovingMode; }

            #endregion Public Properties

            #region Public Methods

            public static void RegisterToViewModelEvent(ViewModelEvent vmEvent, EmptyEventHandler handler)
            {
                eventSubscribers[vmEvent].Add(handler);
            }

            public static void UnregisterToViewModelEvent(ViewModelEvent vmEvent, EmptyEventHandler handler)
            {
                if (eventSubscribers[vmEvent].Contains(handler))
                {
                    eventSubscribers[vmEvent].Remove(handler);
                }
            }

            public static void RaiseViewModelEvent(BaseViewModel invoker, ViewModelEvent vmEvent)
            {
                if (eventOwners[vmEvent] == invoker.GetType())
                {
                    for (int subscriberIndex = 0; subscriberIndex < eventSubscribers[vmEvent].Count; subscriberIndex++)
                    {
                        eventSubscribers[vmEvent][subscriberIndex]();
                    }
                }
            }

            #endregion Public Methods
        }

        #endregion Protected Classes
    }
}
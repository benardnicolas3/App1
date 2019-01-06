Imports Windows.ApplicationModel.Core
''' <summary>
''' Provides application-specific behavior to supplement the default Application class.
''' </summary>
NotInheritable Class App
    Inherits Application

    ''' <summary>
    ''' Invoked when the application is launched normally by the end user.  Other entry points
    ''' will be used when the application is launched to open a specific file, to display
    ''' search results, and so forth.
    ''' </summary>
    ''' <param name="e">Details about the launch request and process.</param>
    Protected Overrides Sub OnLaunched(e As Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)
        Dim rootFrame As Frame = TryCast(Window.Current.Content, Frame)

        ' Do not repeat app initialization when the Window already has content,
        ' just ensure that the window is active.
        If rootFrame Is Nothing Then
            ' Create a Frame to act as the navigation context and navigate to the first page.
            rootFrame = New Frame()

            If e.PreviousExecutionState = ApplicationExecutionState.Terminated Then
                ' If required, load state from previously suspended application.
            End If
            Window.Current.Content = rootFrame
        End If

        If e.PrelaunchActivated = False Then
            If rootFrame.Content Is Nothing Then
                ' When the navigation stack isn't restored navigate to the first page,
                ' configuring the new page by passing required information as a navigation parameter.
                rootFrame.Navigate(GetType(MainPage), e.Arguments)
            End If

            Window.Current.Activate()
        End If
    End Sub

    ''' <summary>
    ''' Invoked when application execution is being suspended.  Application state is saved
    ''' without knowing whether the application will be terminated or resumed with the contents
    ''' of memory still intact.
    ''' </summary>
    ''' <param name="sender">The source of the suspend request.</param>
    ''' <param name="e">Details about the suspend request.</param>
    Private Sub OnSuspending(sender As Object, e As SuspendingEventArgs) Handles Me.Suspending
        Dim deferral As SuspendingDeferral = e.SuspendingOperation.GetDeferral()
        ' TODO: Save application state and stop any background activity
        deferral.Complete()
    End Sub

    ''' <summary>
    ''' When a user launches your app normally (for example, by tapping the app tile), 
    ''' only the OnLaunched method is called. The OnActivated method is called when the 
    ''' app is activated in some other way (including for command-line activation).
    ''' You can discover how the app was activated through the IActivatedEventArgs.Kind property.
    ''' </summary>
    ''' <param name="args">The reason for activating the app, and the previous state.</param>
    Protected Overrides Sub OnActivated(ByVal args As IActivatedEventArgs)
        Dim activationArgString As String = String.Empty
        Dim activationPath As String = String.Empty
        Dim cmdLineString As String = String.Empty

        Select Case args.Kind
            ' The LaunchActivatedEventArgs.Arguments are provided by the app itself.
            Case ActivationKind.Launch
                Dim launchArgs As LaunchActivatedEventArgs = args
                activationArgString = launchArgs.Arguments
            Case ActivationKind.CommandLineLaunch
                Dim cmdLineArgs As CommandLineActivatedEventArgs = args
                Dim operation As CommandLineActivationOperation = cmdLineArgs.Operation

                ' The arguments supplied on command-line activation are available in the 
                ' CommandLineActivationOperation.Arguments property. Note that because these
                ' are supplied by the caller, they should be treated as untrustworthy.
                cmdLineString = operation.Arguments

                ' The CommandLineActivationOperation.CurrentDirectoryPath Iis the directory
                ' current when the command-line activation request was made. This is typically
                ' not the install location of the app itself, but could be any arbitrary path.
                activationPath = operation.CurrentDirectoryPath

                ' TODO Parse the incoming command-line arguments string, as required.

                ' FINITE WORK PATTERN
                ' You can choose to do some finite work and then possibly exit.
                ' If you want to perform asynchronous work that must complete before returning to
                ' the caller, you should take a deferral before exiting.
                'Using deferral As Deferral = operation.GetDeferral()
                    ' TODO Do any asynchronous work within the scope of a deferral.

                    ' The app can supply an exit code to the caller, if required.
                    ' This will be available when OnActivate returns or this app exits.
                    ' If you don't set the ExitCode, it defaults to zero.
                '    operation.ExitCode = 1
                'End Using
                ' If you don't want normal windowed execution, you can now exit.
                'CoreApplication.Exit()
                
                ' REGULAR WINDOWS PATTERN
                ' You can choose to run your normal windowed operation.
                ' Ensure you have a main window set up, and optionally pass in the command-line arguments.
                Dim rootFrame As Frame = TryCast(Window.Current.Content, Frame)
                If rootFrame Is Nothing Then
                    rootFrame = New Frame()
                    Window.Current.Content = rootFrame
                End If
                rootFrame.Navigate(GetType(MainPage), cmdLineString)
                Window.Current.Activate()

            Case Else
                Exit Select
        End Select
    End Sub
    
End Class

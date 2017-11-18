# InternshipDemo

## Notification System
I have created a notification system for Unity3d which can generate custom notifications.

### Two different setups
Implementation 1 has less control over the setup of the generated notifications, but is easier to maintain as everything is set inside the ```NotificationManager``` class.

With implementation 2 a programmer has control over each individual notification class and how it is created. This is done by moving the creating method to the ```Notification``` class itself and abstracting it to it's children.

### Features

#### Easily setup predefined notifications
![alt text](https://github.com/Link-SD/InternshipDemo/blob/master/Documentation/Easy_Fixed_Notifications.png "Easy setup notifications")
```c#
private void ShowPredefinedNotification() {
  NotificationManager.ShowNotification<ToastNotification>("success");
}
```

#### Easily create custom notifications on the fly
```c#
private void ShowCustomNotification() {
  UnityEvent specialEvent = new UnityEvent();
  specialEvent.AddListener(ShowOtherKindOfNotifications);
  Notification.Data data = new Notification.Data("", specialEvent, "Very Cool Title", "Best Description ever.");
  NotificationManager.ShowNotification<ModalNotification>(data);
}
```
#### Bind custom functionality to notifications
![alt text](https://github.com/Link-SD/InternshipDemo/blob/master/Documentation/Easy_Bind_Functionality.png "Bind functionality")

#### Classes
![alt text](https://github.com/Link-SD/InternshipDemo/blob/master/Documentation/NotificationSystem_InheritenceGraph.png "Class diagram")

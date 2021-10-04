When you launch this application for the first time, you will see Login panel, where you can print down your login/password or press "Registration" button if you don`t have account. 

There is 6 accounts from start in database, 1 admin and 5 users.
List of them:
Login       Password
admin       123456
1           123
12          123
123         123
1234        123
12345       123

| Login  | Password |
| ------------- | ------------- |
| admin  | 123456  |
| 1  | 123  |
| 12  | 123  |
| 123  | 123  |
| 1234  | 123  |
| 12345  | 123  |

If you login as user, you will be redirected to your reservated workplace or, if you didn't reserve it, to reservation panel. 
If you already reserved workplace, you will see its name and list of your devices.
If you don't have your workplace, you will see list of all worplaces in database, id of them, their description and see, is it used or not.
Below you can choose free workplace and list of devices that you need. After pressing "Choose" button, you will be redirected to your reserved place.

If you login as administrator, you will see 4 buttons:
1. Add workplaces
2. Edit workplaces
3. Delete workplaces
4. Delete reservations
If you click on first one, you will see list of all workplaces and some extra information about them: ReservationId and list of devices, chosen by user.
Below you can add a new workplace by typing its Description in label. Also, you can choose devices, that will be at this place, if you want. 
When you press "Add", you will add workplace to the database.

If you click on second one, you will see all workplaces and buttons below. You can choose table by id, type new Description for it and manage list of devices. 
After pressing "Edit" button all of this will be edited in database.

If you press third button, you will see list of workplaces and buttons below. You can choose workplace by id and delete it even if user already reserved it. 
After this user will have to reserve another table if he want to continue working.

After you press fourth one, you will see list of reservations. It is empty by default, because all workplaces are free after creation. 
If user reserve workplace, you will see id of reservation, id of employee and date of reservation. 
Usually, reservation lasts till next day, but admin can delete it manually, using buttons below. 
If you delete user's reservation, he will have to reserve workplace again, because user's workplace become free.

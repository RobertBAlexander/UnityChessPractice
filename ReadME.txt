If I use the Kevin MacLeod music:
Music from https://filmmusic.io
"Lost Time" by Kevin MacLeod (https://incompetech.com)
License: CC BY (http://creativecommons.org/licenses/by/4.0/)

There are three aspects to the online. 

1)The need to connect two players together.
-Connecting players should be done pier to pier, where the two players games are essentially emails eachother information.
This means that we need to store some amount of information about that player's current games and moves somewhere though.
-----Needs solving through good code?----

The need to store information about players.
--an online leaderboard, but also more than that.
You want a leaderboard that shows top players, also top players in friends list, and top players by region.
Need to record number of games played.
Record status of all current games(something similar to wargrove)
-----Needs solving through database of some sort---
Simplest:
http://dreamlo.com/
Online leaderboard tracker. Integrates with game. Just highscore and username.

https://forum.unity.com/threads/easy-achievements-and-leaderboards-engage-your-players.534278/

https://medium.com/teamarimac/how-to-develop-a-complete-leader-board-for-a-unity-game-using-firebase-facebook-sign-in-google-d865f577b303
Google play Games. Compatible with IOS, but is is with PC, other platforms? Seems to be mobile game only.


https://playfab.com/
Handles all storage for you. Unclear on costs, but seems to be $60 a month once you hit a certain level. Not sure below that.

https://github.com/rankpin/rankpin
code to set up your rankings. Unclear if you need your own server, but I assume so. Ugh!

https://www.youtube.com/watch?v=JKmjk6d5FgM
https://support.pubnub.com/support/solutions/articles/14000046386-what-is-pubnub-
also paid for service. Need to compare prices.


The ability for players to log in to their location/account.
A google log in. This probably doesn't store info, so would need to call the info stored in step 2.
-----Needs solving through Google login(possibly alt version of login?)----

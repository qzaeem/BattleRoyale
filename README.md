# BattleRoyale
This is a demo project intended to get an idea of my general coding style.

Project Description

UI
- A maximum of three heroes (players) can be selected for a battle. Selected heroes are shown in yellow color.
- If a click is held for three seconds on a hero, the attributes for that hero are displayed.
- Health is displayed below each character.

Heroes
- A minimum of three heroes (Players) are selected to start the battle against an enemy.
- Heroes have attributes: name, health, attack power, experience and level.
- Heroes can be more than three, but only three can be selected for a battle.
- A new hero is unlocked after every 5 battles.
- The max number of heroes is 10.
- A selected hero is shown in yellow color.

Battle
- Three heroes are to be selected for a battle.
- Once a battle is started, player can select a single hero to attack the enemy. The hero moves towards the enemy, attacks it, and some damage is inflicted upon the enemy.
- When it's enemy's turn, the enemy does the same: selects a random player and attacks it.
- This goes on until all the players or the enemy have their health reduced to zero.
- The result is displayed.

Progress
- After every fifth battle, whether won or lost, a new hero is unlocked. A maximum of 10 heroes can be collected.
- Every battle won will increase the experience attribute by 1 of only the alive heroes.
- Every five experience points will increase hero's level +1.
- Each level increase increases hero's health by 10%.
- Game progress persists; player prefs are used.

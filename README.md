I spent just over three hours in total on this challenge, the orginial readme said to spend a "few hours".

I didn't get to implement all of the requirements outlined in the specification, here are a few missing features:
* No submit button for ship instructions: this would have been an Ajax call via jQuery to the controller action /ShipTracking/MoveShips
* In lieu of this, the method can be accessed directly via a GET with some instructions hard-coded in the controller method
* As such the validation for any invalid input will not be displayed on the view.
* No way to add ships
* Error handling is sparse: I would have made use of my UpdateAttempt class to handle any errors and return them to the user
* Grid cannot be edited, however the method to update the grid's data file (json) is there - would have just set the coordinates in a form post via Ajax
* As such, grid is not enforced as a rectangle
* Grid can be edited manually by editing the JSON
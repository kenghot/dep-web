﻿function Welcome(person: string) {
    return "<h2>Hello " + person + ", Lets learn TypeScript</h2>";
}
//test
function ClickMeButton() {
    let user: string = "MithunVP";
    document.getElementById("divMsg").innerHTML = Welcome(user);
}
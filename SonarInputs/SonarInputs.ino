/*
  Input Pull-up Serial

  This example demonstrates the use of pinMode(INPUT_PULLUP). It reads a digital
  input on pin 2 and prints the results to the Serial Monitor.

  The circuit:
  - momentary switch attached from pin 2 to ground
  - built-in LED on pin 13

  Unlike pinMode(INPUT), there is no pull-down resistor necessary. An internal
  20K-ohm resistor is pulled to 5V. This configuration causes the input to read
  HIGH when the switch is open, and LOW when it is closed.

  created 14 Mar 2012
  by Scott Fitzgerald

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/InputPullupSerial
*/

const int ECHO1 = 12;
const int TRIG1 = 11;
const int ECHO2 = 6;
const int TRIG2 = 5;

void setup() {
  //start serial connection
  Serial.begin(9600);
  //configure pin 2 as an input and enable the internal pull-up resistor
  pinMode(TRIG1, OUTPUT);
  pinMode(ECHO1, INPUT);
  pinMode(TRIG2, OUTPUT);
  pinMode(ECHO2, INPUT);
  pinMode(LED_BUILTIN, OUTPUT);

}

void loop() {
  digitalWrite(TRIG1, LOW);
  delayMicroseconds(3);

  digitalWrite(TRIG1, HIGH);
  delayMicroseconds(10);
  digitalWrite(TRIG1, LOW);

  int duration1 = pulseIn(ECHO1, HIGH, 20000);

  if (duration1 == 0) {
    pinMode(ECHO1, OUTPUT);
    digitalWrite(ECHO1, LOW);
    delayMicroseconds(200);
    pinMode(ECHO1, INPUT);
  }
  int distance1 = duration1 * 0.034/2;

  if (distance1 > 0 && distance1 < 50) {
    Serial.println(1);
  }
  digitalWrite(TRIG2, LOW);
  delayMicroseconds(3);
  digitalWrite(TRIG2, HIGH);
  delayMicroseconds(10);
  digitalWrite(TRIG2, LOW);
  int duration2 = pulseIn(ECHO2, HIGH, 20000);

  if (duration2 == 0) {
    pinMode(ECHO2, OUTPUT);
    digitalWrite(ECHO2, LOW);
    delayMicroseconds(200);
    pinMode(ECHO2, INPUT);
  }
  int distance2 = duration2 * 0.034/2;
  if (distance2 > 0 && distance2 < 50){
    Serial.println(2);
    digitalWrite(LED_BUILTIN, HIGH);
  } else
    digitalWrite(LED_BUILTIN, LOW);
  if ((distance1 <= 0 || distance1 >= 50) && (distance2 <= 0 || distance2 >= 50)){
    Serial.println(0);
  }
}

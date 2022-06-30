#include <Arduino_LSM6DS3.h>
#include <WiFiNINA.h>


const int vibrator = 12;
const int trigger = 11;
const int reload = 10;
const int led = 2;

const int finger[4] = { 6, 7, 8, 9};

bool on = false;
bool pressed = false;

unsigned long last_time = 0;

int count = 0, timer = 50000;

float x, y, z;

int degreeX, degreeY;

float xT = -10.0, yT = -10.0, zT = -10.0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  IMU.begin();

  pinMode(vibrator, OUTPUT);
  pinMode(trigger, INPUT_PULLUP);
  pinMode(reload, INPUT_PULLUP);
  pinMode(led, OUTPUT);

  pinMode(finger[0], INPUT_PULLUP);
  pinMode(finger[1], INPUT_PULLUP);  
  pinMode(finger[2], INPUT_PULLUP);  
  pinMode(finger[3], INPUT_PULLUP);  

  
}

void loop() {  
  // put your main code here, to run repeatedly:
  //Print a heartbeat
  // if (millis() > last_time + 2000)
  // {
  // Serial.println("Arduino is alive!!");
  // last_time = millis();
  // }
  // //Send some message when I receive an 'A' or a 'Z'.
  // switch (Serial.read())
  // {
  //   case 'A':
  //     Serial.println(2);
  //     break;
  //   case 'Z':
  //     Serial.println(3);
  //     break;
  // } 

  // if(digitalRead(trigger) == LOW && !pressed){
  //   pressed = true;
  //   toogle();
  // }
  // if(digitalRead(trigger) == HIGH && pressed){
  //   pressed = false;
  // }


  // if(on){
  //   digitalWrite(led, HIGH);
  //   digitalWrite(vibrator, HIGH);
  // }
  // else{
  //   digitalWrite(led, LOW);
  //   digitalWrite(vibrator, LOW);
  // }


  if(count == 0)
  {
    fingers();
    //accel();
    buttons();

    gyro();

    Serial.println(' ');
  }

  count++;

  if(count == timer) count = 0;

}

void toogle(){
  on = !on;
}

void fingers(){
  for(int i=0; i<4; i++){
    if(digitalRead(finger[i]) == LOW){  
      Serial.print("CLOSED");    
    }
    else{
      Serial.print("OPEN");
    }
    Serial.print(' ');
  }
}

void gyro(){
  if (IMU.gyroscopeAvailable()){
    IMU.readGyroscope(x,y,z);

    // if(x < xT || x > 0) 
    // {
    //   Serial.print("X: ");
    //   Serial.print(x);
    //   Serial.print(' ');
    // }
    
    // if(y < yT || y > 0) 
    // {
    //   Serial.print("Y: ");
    //   Serial.print(y);
    //   Serial.print(' ');
    // }
    // if(z < zT || z > 0) 
    // {
    //   Serial.print("Z: ");
    //   Serial.print(z);
    // }

    // if(x < xT || x > 0) Serial.print(x*-1);
    // else Serial.print(0);

    Serial.print(x*-1);

    Serial.print(' ');
    
    // if(y < yT || y > 0) Serial.print(y);
    // else Serial.print(0);

    Serial.print(y);

    Serial.print(' ');

    // if(z < zT || z > 0) Serial.print(z);
    // else Serial.print(0);

    Serial.print(z);

    Serial.print(' ');

    

    // if(y > plusThreshold)
    // {
    //   Serial.println("Collision front");
    //   delay(500);
    // }
    // if(y < minusThreshold)
    // {
    //   Serial.println("Collision back");
    //   delay(500);
    // }
    // if(x < minusThreshold)
    // {
    //   Serial.println("Collision right");
    //   delay(500);
    // }
    //   if(x > plusThreshold)
    // {
    //   Serial.println("Collision left");
    //   delay(500);
    // }    
  }
}

void accel(){
  if (IMU.accelerationAvailable()) {
    float w;
    IMU.readAcceleration(x, y, z);

    x*=100;
    y*=100;

    if(z >= 0)
    {
      degreeX = map(x, -100, 100, -180, 0);
      degreeY = map(y, -100, 100, -180, 0);
    }
    else
    {
      degreeX = map(x, 100, -100, 0, 180);
      degreeY = map(y, 100, -100, 0, 180);
    }

    Serial.print(degreeX);
    //Serial.print(x);
    Serial.print(' ');
    Serial.print(degreeY);
    // Serial.print(y);
    // Serial.print(' ');
    // Serial.print(z);
    // Serial.print(' ');
    // Serial.println(w);
  }
}

void buttons(){
  if(digitalRead(trigger) == LOW){
    Serial.print("FIRE");
    shoot(true);
  }
  else{
    Serial.print("OFF");
    shoot(false);
  }

  Serial.print(" ");
  
  if(digitalRead(reload) == LOW){
    Serial.print("RELOAD");
  }
  else{
    Serial.print("OFF");
  }

  Serial.print(" ");
}

void shoot(bool v)
{
  if(v)
  {
    digitalWrite(vibrator, HIGH);
    digitalWrite(led, HIGH);
  }
  else 
  {
    digitalWrite(vibrator, LOW);
    digitalWrite(led, LOW);
  }
   
}


const int ledPin = 13; //pin 13 built-in led
const int soundPin = A0; //sound sensor attach to A0

void setup() {
  // put your setup code here, to run once:
  pinMode(ledPin,OUTPUT);//set pin13 as OUTPUT
  Serial.begin(9600); //initialize seria
}

void loop() {
  // put your main code here, to run repeatedly:
  int value = analogRead(soundPin);//read the value of A0
  Serial.println(value);//print the value
  if(value >= 800) //if the value is greater than 600
  {
    digitalWrite(ledPin,HIGH);//turn on the led
    delay(500);//delay 200ms
  }
  else
  {
    digitalWrite(ledPin,LOW);//turn off the led    
  }
}

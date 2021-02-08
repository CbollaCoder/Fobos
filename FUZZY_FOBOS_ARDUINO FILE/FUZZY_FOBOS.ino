#include <ESP8266WiFi.h>
#include <Fuzzy.h>

#include <OneWire.h>
#include <DallasTemperature.h>

///////////////////////////////CONEXION A RED

//const char* ssid     = "HUAWEI P20 lite";
const char* ssid     = "Cayro";

//const char* password = "ekisde12345";
const char* password = "limonrico";

//const char* host = "192.168.43.183";
const char* host = "192.168.0.13";

//Sensor de Temperatura
//#define ONE_WIRE_BUS 5
const int oneWireBus = 4;
//OneWire oneWire(ONE_WIRE_BUS);
OneWire oneWire(oneWireBus);
DallasTemperature sensors(&oneWire);

// Pulso cardiaco
int PulseSensor = A0 ;
int Threshold = 450;
int cont=0;

String res;

// For scope, instantiate all objects you will need to access in loop()
// It may be just one Fuzzy, but for demonstration, this sample will print
// all FuzzySet pertinence

// Fuzzy
Fuzzy *fuzzy = new Fuzzy();

// FuzzyInput
FuzzySet *NormalF = new FuzzySet(60, 60, 70, 85);
FuzzySet *Rapido = new FuzzySet(80, 85, 85, 90);
FuzzySet *MuyRapido = new FuzzySet(85, 90, 90, 95);
FuzzySet *Acelerado = new FuzzySet(90, 95, 95, 100);
FuzzySet *MuyAcelerado = new FuzzySet(95, 98, 100, 100);

// FuzzyInput
FuzzySet *NormalT = new FuzzySet(32, 32.5, 33.5, 33.5);
FuzzySet *Suficiente = new FuzzySet(31.75, 32, 32, 32.5);
FuzzySet *Escaso = new FuzzySet(31.5, 31.75, 31.75, 32);
FuzzySet *Tenue = new FuzzySet(31.25, 31.5, 31.5, 31.75);
FuzzySet *Frio = new FuzzySet(31, 31, 31.25, 31.5);

// FuzzyOutput
FuzzySet *RelajacionT = new FuzzySet(0, 0, 1, 3);
FuzzySet *Calma = new FuzzySet(2, 3, 3, 5);
FuzzySet *Ansiedad = new FuzzySet(4, 5, 5, 7);
FuzzySet *AnsiedadElevada = new FuzzySet(6, 7, 7, 9);
FuzzySet *PanicoTotal = new FuzzySet(8, 9, 10, 10);

void setup(void)
{
    // Set the Serial output
  Serial.begin(115200);
  sensors.begin();
  
  //////////////////////////////////////////////////////////CONEXION A RED
  /*Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);*/

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  /*Serial.println("");
  Serial.println("WiFi connected");  
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());*/
  /////////////////////////////////////////////////////////
  // Every setup must occur in the function setup()
  // FuzzyInput
  FuzzyInput *frecuencia = new FuzzyInput(1);

  frecuencia->addFuzzySet(NormalF);
  frecuencia->addFuzzySet(Rapido);
  frecuencia->addFuzzySet(MuyRapido);
  frecuencia->addFuzzySet(Acelerado);
  frecuencia->addFuzzySet(MuyAcelerado);
  fuzzy->addFuzzyInput(frecuencia);

  // FuzzyInput
  FuzzyInput *temperatura = new FuzzyInput(2);

  temperatura->addFuzzySet(NormalT);
  temperatura->addFuzzySet(Suficiente);
  temperatura->addFuzzySet(Escaso);
  temperatura->addFuzzySet(Tenue);
  temperatura->addFuzzySet(Frio);
  fuzzy->addFuzzyInput(temperatura);

  // FuzzyOutput
  FuzzyOutput *miedo = new FuzzyOutput(1);

  miedo->addFuzzySet(RelajacionT);
  miedo->addFuzzySet(Calma);
  miedo->addFuzzySet(Ansiedad);
  miedo->addFuzzySet(AnsiedadElevada);
  miedo->addFuzzySet(PanicoTotal);
  fuzzy->addFuzzyOutput(miedo);

  // Building FuzzyRule
  FuzzyRuleAntecedent *sifrecuenciaNormalyTemperaturaNormal = new FuzzyRuleAntecedent();
  sifrecuenciaNormalyTemperaturaNormal->joinWithAND(NormalF, NormalT);

  FuzzyRuleConsequent *entoncesRelajacionTotal = new FuzzyRuleConsequent();
  entoncesRelajacionTotal->addOutput(RelajacionT);

  FuzzyRule *fuzzyRule1 = new FuzzyRule(1, sifrecuenciaNormalyTemperaturaNormal,entoncesRelajacionTotal );
  fuzzy->addFuzzyRule(fuzzyRule1);

   // Building FuzzyRule
  FuzzyRuleAntecedent *sifrecuenciaRapidoyTemperaturaSuficiente = new FuzzyRuleAntecedent();
  sifrecuenciaRapidoyTemperaturaSuficiente->joinWithAND(Rapido, Suficiente);

  FuzzyRuleConsequent *entoncesCalma = new FuzzyRuleConsequent();
  entoncesCalma->addOutput(Calma);

  FuzzyRule *fuzzyRule2 = new FuzzyRule(2, sifrecuenciaRapidoyTemperaturaSuficiente,entoncesCalma );
  fuzzy->addFuzzyRule(fuzzyRule2);

     // Building FuzzyRule
  FuzzyRuleAntecedent *sifrecuenciaMuyRapidoyTemperaturaEscaso = new FuzzyRuleAntecedent();
  sifrecuenciaMuyRapidoyTemperaturaEscaso->joinWithAND(MuyRapido, Escaso);

  FuzzyRuleConsequent *entoncesAnsiedad = new FuzzyRuleConsequent();
  entoncesAnsiedad->addOutput(Ansiedad);

  FuzzyRule *fuzzyRule3 = new FuzzyRule(3, sifrecuenciaMuyRapidoyTemperaturaEscaso,entoncesAnsiedad);
  fuzzy->addFuzzyRule(fuzzyRule3);

   // Building FuzzyRule
  FuzzyRuleAntecedent *sifrecuenciaAceleradoyTemperaturaTenue = new FuzzyRuleAntecedent();
  sifrecuenciaAceleradoyTemperaturaTenue->joinWithAND(Acelerado, Tenue);

  FuzzyRuleConsequent *entoncesAnsiedadElevada = new FuzzyRuleConsequent();
  entoncesAnsiedadElevada->addOutput(AnsiedadElevada);

  FuzzyRule *fuzzyRule4 = new FuzzyRule(4, sifrecuenciaAceleradoyTemperaturaTenue,entoncesAnsiedadElevada);
  fuzzy->addFuzzyRule(fuzzyRule4);

   // Building FuzzyRule
  FuzzyRuleAntecedent *sifrecuenciaMuyAceleradoyTemperaturaFrio = new FuzzyRuleAntecedent();
  sifrecuenciaMuyAceleradoyTemperaturaFrio->joinWithAND(MuyAcelerado, Frio);

  FuzzyRuleConsequent *entoncesPanicoTotal = new FuzzyRuleConsequent();
  entoncesPanicoTotal->addOutput(PanicoTotal);

  FuzzyRule *fuzzyRule5 = new FuzzyRule(5, sifrecuenciaMuyAceleradoyTemperaturaFrio,entoncesPanicoTotal);
  fuzzy->addFuzzyRule(fuzzyRule5);

}

int value = 0;

void loop(void)
{
  //115
   for(int i=1;i<=30;i++)
   {
       sensors.requestTemperatures();
       int Signal = analogRead(PulseSensor);  //Lectura de datos del sensor de ritmo cardiaco
       if(Signal > Threshold)
       {
       cont++;
       }    
   }

  int input1 = cont*2;
  float input2 = sensors.getTempCByIndex(0);

  /*Serial.println("Entrada: ");
  Serial.println("Frecuencia cardiaca: ");
  Serial.println(input1);
  Serial.println("Temperatura de la piel: ");
  Serial.println(input2);*/

  fuzzy->setInput(1, input1);
  fuzzy->setInput(2, input2);

  fuzzy->fuzzify();

  /*Serial.println("");
  Serial.println("Frecuencia: Normal-> ");
  Serial.println(NormalF->getPertinence());
  Serial.println("Rapido-> ");
  Serial.println(Rapido->getPertinence());
  Serial.println("Muy rapido-> ");
  Serial.println(MuyRapido->getPertinence());
  Serial.println("Acelerado-> ");
  Serial.println(Acelerado->getPertinence());
  Serial.println("Muy Acelerado-> ");
  Serial.println(MuyAcelerado->getPertinence());

  Serial.println("");
  Serial.println("Temperatura: Normal-> ");
  Serial.println(NormalT->getPertinence());
  Serial.println("Suficiente-> ");
  Serial.println(Suficiente->getPertinence());
  Serial.println("Escaso-> ");
  Serial.println(Escaso->getPertinence());
  Serial.println("Tenue-> ");
  Serial.println(Tenue->getPertinence());
  Serial.println("Frio-> ");
  Serial.println(Frio->getPertinence());*/

  float output1 = fuzzy->defuzzify(1);

  /*Serial.println(" ");
  Serial.println("Salida: ");
  Serial.println("Miedo: Relajacion Total-> ");
  Serial.println(RelajacionT->getPertinence());
  Serial.println("Calma-> ");
  Serial.println(Calma->getPertinence());
  Serial.println("Ansiedad-> ");
  Serial.println(Ansiedad->getPertinence());
  Serial.println("Ansiedad elevada-> ");
  Serial.println(AnsiedadElevada->getPertinence());
  Serial.println("Pánico Total-> ");
  Serial.println(PanicoTotal->getPertinence());

  Serial.println(" ");
  Serial.println("Resultado: ");
  Serial.println("Miedo: ");
  Serial.println(output1);*/

  int out = round(output1);

  if( out >= 0 && out <= 2){
    res="RELAJACION-TOTAL";
    //Serial.println(res);
  }

  if(out > 2.00 && out <= 4){
    res="CALMA";
    //Serial.println(res);
  }

  if(out > 4.00 && out <= 6){
    res="ANSIEDAD";
    //Serial.println(res);
  }

  if(out > 6.00 && out <= 8){
    res="ANSIEDAD-ELEVADA";
    //Serial.println(res);
  }

  if(out > 8.00 && out <= 10){
    res="PANICO-TOTAL";
    //Serial.println(res);
  }
  
  //Serial.println("----------------------------------------------------------------- ");

  cont=0; 

  //////////////////////////////////////////////////////////////////////////////////// CONEXION A SERVIDOR
  delay(2000);
  ++value;

  /*Serial.print("connecting to ");
  Serial.println(host);*/

  // Creamos una instancia de WIFICLIENT 
  WiFiClient client;
  const int httpPort = 80;
  if (!client.connect(host, httpPort)) {
    Serial.println("connection failed");
    return;
  }

  // Creamos la direccion para luego usarla en el String del POST que tendremos que enviar
  String url = "http://192.168.0.13/Prueba/prueba.php";
  //String url = "http://192.168.43.183/Prueba/prueba.php";
  // creo un string con los datos que enviamos por POST 
 //String data ="Frecuencia cardiaca="+String (input1);
 //String data1 ="Temperatura de la piel="+String (input2);
 String data ="res="+String (res); // RES DEL ARCHIVO HTML
 
  //imprimo la url a donde enviaremos la solicitud, solo para debug
  /*Serial.print("Requesting URL: ");
  Serial.println(url);*/

  // Esta es la solicitud del tipoPOST que enviaremos al servidor
   client.print(String("POST ") + url + " HTTP/1.0\r\n" +
               "Host: " + host + "\r\n" +
               "Accept: *" + "/" + "*\r\n" +
               "Content-Length: " + data.length() + "\r\n" +
               "Content-Type: application/x-www-form-urlencoded\r\n" +
               "\r\n" + data);

 delay(500);

  // Leemos todas las lineas que nos responde el servidor y las imprimimos por pantalla, esto no es necesario  pero es fundamental ver quÃ¨ me responde el servidor para localizar fallas en la solicitud post que estoy enviando, 
  //Serial.println("Respond:");
  while(client.available()){
    String line = client.readStringUntil('\r');
    //Serial.print(line);
  }

  //Serial.println();

  // se cierra la conexion
  //Serial.println("closing connection");
  
  Serial.println(String(input1)+" "+String(input2)+" "+res);
  delay(500);
}

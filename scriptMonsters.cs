using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class scriptMonsters : MonoBehaviour {
	
	public TextAsset xmlMonsters;
	public TextAsset xmlAttacks;
	public TextAsset xmlQuirks;


	XmlDocument xmlDoc = new XmlDocument();
	XmlDocument xmlAtk = new XmlDocument();
	XmlDocument xmlQuirk = new XmlDocument();


	//instances
	public string[] list;
	public int[] party;
	public int[] level;
	public int[] sprites;
	public int[] palettes;
	public string[] nicknames;
	public string[] instanceSprites;
	public string[] instancePalettes;
	public int[] hp;
	public int[] currentHP;
	public string[] currentStatus;
	public float[] atk;
	public float[] mag;
	public float[] spd;
	public float [] def;
	public float[] res;
	public double[] exp;
	public string[] attacks;
	public string[] attr;
	public string[] quirk;

	public double[] hpSeed;
	public double[] atkSeed;
	public double[] magSeed;
	public double[] spdSeed;
	public double[] resSeed;
	public double[] defSeed;

	public string[] followType;

	//genetics
	public string[] learnSet;
	public float[] growthRate;

	//"dex"
	public Sprite[] dSprites;
	public Texture2D[] dPalettes;

	void Start() {
		//load monsters data
		string xml = xmlMonsters.text;
		xmlDoc.LoadXml (xml);

		//load attack data
		string xml1 = xmlAttacks.text;
		xmlAtk.LoadXml (xml1);

		//load quirk data
		string xml2 = xmlQuirks.text;
		xmlQuirk.LoadXml (xml2);
	}

	public int[] GetPartyMonsters(){
		return party;
	}

	public string[] GetMonsters(){
		return list;
	}

	public string IndexToName(int id){ //gets instance id, convert to monster id, lookup name

		string name="";

		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);

		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString()){
				name = nodes[i].ChildNodes[2].InnerText;
			}
		}

		Debug.Log ("parsed and found: "+name);

		return name;
	}

	public string InstanceToName(int instanceID){
		return nicknames[instanceID];
	}

	public void InstanceSetName(int i,string n){
		nicknames[i] = n;
	}

	public int InstanceToHP(int instanceId){ //gets instance id, convert to monster id, lookup name
		Debug.Log ("fetching HP for id " + instanceId + "found HP:"+hp[instanceId]);
		return hp[instanceId];
	}

	public int InstanceToCurrentHP(int instanceId){ //gets instance id, convert to monster id, lookup name
		return currentHP[instanceId];
	}

	public void InstanceSetCurrentHP(int id,int val){
		currentHP[id] = val;
	}

	public string InstanceToStatus(int instanceID){
		return currentStatus[instanceID];
	}

	public void InstanceSetHP(int i,int h){
		hp[i] = h;
	}

	public int InstanceToLvl(int instanceId){ //gets instance id, convert to monster id, lookup name
		Debug.Log ("fetching lvl for id " + instanceId + "found lvl:"+hp[instanceId]);
		return level[instanceId];
	}

	public Sprite InstanceToSprite(int instanceId){

		Sprite spr=null;

		spr = IndexToSprite(sprites[instanceId]);

		return spr;
	}

	public Sprite IndexToSprite(int index){
		
		Sprite spr=null;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == index.ToString ()){
				spr = dSprites[i];
			}
		}
		
		return spr;
	}

	public int InstanceToPaletteID(int instanceId){
		return palettes[instanceId];
	}

	public Texture2D IndexToPalette(int id){

		return dPalettes[id];
	}

	public int IndexToPaletteID(int id){
	
		string tempPal="";
		int pal;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(int.Parse (nodes[i].ChildNodes[0].InnerText) == id){
				tempPal = nodes[i].ChildNodes[1].InnerText;
			}
		}

		pal = int.Parse (tempPal);

		Debug.Log ("parsed and found: "+pal);
		
		return pal;
	}

	public int InstanceToSpd(int instanceID){
		int speed=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		string realID = list[instanceID];
		Debug.Log ("discerned ID " + realID.ToString ());
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == realID){
				speed = int.Parse(nodes[i].ChildNodes[9].InnerText);
			}
		}
		
		Debug.Log ("parsed and found: "+speed);
		
		return speed;
	}

	public void InstanceSetSpd(int i, int amt){
		spd[i] = amt;
	}

	public int InstanceToIndex(int instanceID){
		int index=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		string realID = list[instanceID];
		Debug.Log ("discerned ID " + realID.ToString ());
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == realID){
				index = int.Parse(nodes[i].ChildNodes[0].InnerText);
			}
		}
		
		return index;
	}

	public string IndexToAttr(int id){
		string type="";
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				type = nodes[i].ChildNodes[4].InnerText;
			}
		}
		
		return type;
	}

	public string InstanceToAttr(int instanceID){

		return attr[instanceID];
	}

	public int IndexToHP(int id){
		int hp=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);

		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				hp = int.Parse(nodes[i].ChildNodes[5].InnerText);
			}
		}
		
		return hp;
	}

	public double IndexToAtk(int id){
		double atk=0;

		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				atk = double.Parse(nodes[i].ChildNodes[6].InnerText);
			}
		}
		
		return atk;
	}

	public float InstanceToAtk(int id){
		return atk[id];
	}

	public double IndexToMag(int id){
		double mag=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				mag = double.Parse(nodes[i].ChildNodes[7].InnerText);
			}
		}
		
		return mag;
	}

	public double IndexToSpd(int id){
		double spd=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				spd = double.Parse(nodes[i].ChildNodes[8].InnerText);
			}
		}
		
		return spd;
	}

	public double IndexToDef(int id){
		double def=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				def = double.Parse(nodes[i].ChildNodes[9].InnerText);
			}
		}
		
		return def;
	}

	public double IndexToRes(int id){
		double res=0;
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id.ToString ()){
				res = double.Parse(nodes[i].ChildNodes[10].InnerText);
			}
		}
		
		return res;
	}

	public string InstanceToAttacks(int id){
		return attacks[id];
	}

	public void InstanceSetAttacks(int id,string val){
		attacks[id] = val;
	}

	public float InstanceToSpeed(int id){
		return spd[id];
	}

	public void InstanceSetSpeed(int id, int amt){
		spd[id] = amt;
	}

	public string IndexToAttackName(string id){

		Debug.Log ("Evaluating attack id for name: "+id);
	
		string atkName="";

		string levelsxpath = "/attacks/attack";
		XmlNodeList nodes = xmlAtk.SelectNodes(levelsxpath);

		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				atkName = nodes[i].ChildNodes[2].InnerText;
			}
		}

		return atkName;
	}

	public int IndexToAttackPower(string id){
		
		int atkPower=0;
		
		string levelsxpath = "/attacks/attack";
		XmlNodeList nodes = xmlAtk.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				atkPower = int.Parse (nodes[i].ChildNodes[3].InnerText);
			}
		}
		
		return atkPower;
	}

	public string IndexToAttackType(string id){
		
		string atkType="";
		
		string levelsxpath = "/attacks/attack";
		XmlNodeList nodes = xmlAtk.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				atkType = nodes[i].ChildNodes[4].InnerText;
			}
		}
		
		return atkType;
	}

	public bool IndextToAttackPhysical(string id){
		bool physical = false;
		
		string levelsxpath = "/attacks/attack";
		XmlNodeList nodes = xmlAtk.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				physical = bool.Parse(nodes[i].ChildNodes[1].InnerText);
			}
		}
		
		return physical;
	}

	public string[] IndexToAttackEffect(string id){
		string[] effect = new string[6];

		string levelsxpath = "/attacks/attack";
		XmlNodeList nodes = xmlAtk.SelectNodes(levelsxpath);

		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				effect[0] = nodes[i].ChildNodes[5].ChildNodes[0].ChildNodes[0].InnerText;
				effect[1] = nodes[i].ChildNodes[5].ChildNodes[0].ChildNodes[1].InnerText;
				effect[2] = nodes[i].ChildNodes[5].ChildNodes[1].ChildNodes[0].InnerText;
				effect[3] = nodes[i].ChildNodes[5].ChildNodes[1].ChildNodes[1].InnerText;
				effect[4] = nodes[i].ChildNodes[5].ChildNodes[2].ChildNodes[0].InnerText;
				effect[5] = nodes[i].ChildNodes[5].ChildNodes[2].ChildNodes[1].InnerText;
			}
		}

		return effect;
	}

	public string IndexToQuirk(string id){
		string quirk = "";
		
		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);
		
		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				int r = Mathf.FloorToInt(Random.Range(0,nodes[i].ChildNodes[12].ChildNodes.Count));
				quirk = nodes[i].ChildNodes[12].ChildNodes[r].InnerText;
			}
		}
		Debug.Log("IndextToQuirk: Found quirk "+quirk);

		return quirk;
	}

	public string IndexToQuirkName(string id){
		string quirkName = "";

		string levelsxpath = "/quirks/quirk";
		XmlNodeList nodes = xmlQuirk.SelectNodes(levelsxpath);

		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				quirkName = nodes[i].ChildNodes[1].InnerText;
			}
		}

		return quirkName;
	}

	public string IndexToQuirkEffect(string id){
		string quirkEffect = "";

		string levelsxpath = "/quirks/quirk";
		XmlNodeList nodes = xmlQuirk.SelectNodes(levelsxpath);

		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].ChildNodes[0].InnerText == id){
				quirkEffect = nodes[i].ChildNodes[2].InnerText;
			}
		}

		return quirkEffect;
	}

	public string InstanceToQuirk(int id){
		return quirk[id];
	}

	public double InstanceToExp(int id){
		return exp[id];
	}

	public void InstanceSetExp(int id, double amt){
		exp[id] = amt;
	}

	public void InstanceSetLvl(int id, int l){
		level[id] = l;
	}

	public string InstanceToLearnset(int id){
		return learnSet[id];
	}

	public string[] IndexToExpSpread(string id){
		string[] spread = new string[4];

		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);

		for(int i=0;i<nodes.Count;i++){
			if(nodes[i].ChildNodes[0].InnerText == id){
				spread[0]= nodes[i].ChildNodes[13].ChildNodes[0].InnerText;
				spread[1] = nodes[i].ChildNodes[13].ChildNodes[1].InnerText;
				spread[2]= nodes[i].ChildNodes[13].ChildNodes[2].InnerText;
				spread[3] = nodes[i].ChildNodes[13].ChildNodes[3].InnerText;
			}
		}

		return spread;
	}

	public string IndexToLearnSet(string id){
		string ls="";

		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);

		for(int i=0;i<nodes.Count;i++){
			if(nodes[i].ChildNodes[0].InnerText == id){
				Debug.Log("IndexToLearnSet: Found "+nodes[i].ChildNodes[11].ChildNodes.Count.ToString()+" entries");
				for(int x=0;x<nodes[i].ChildNodes[11].ChildNodes.Count;x++){
					ls+=nodes[i].ChildNodes[11].ChildNodes[x].InnerText+"-";//attackid-lvl-attackid-level
				}
			}
		}

		return ls;

	}

	public string IndexToMoveset(int lv, string ls){
		//get a usable array and length
		string[] splitMoveset = ls.Split(new string[] { "-" }, System.StringSplitOptions.None);

		Debug.Log("loading moveset "+ls);

		//parse for last 7 moves for this level
		string mS="";
		Debug.Log("Moveset is this long: "+(splitMoveset.Length-1));
		for(int i=splitMoveset.Length-2;i>0;i-=2){
			//if the current parsed move is equal or less than monster's level
			Debug.Log("comparing for level "+splitMoveset[i] + "i is equal to:"+i);
			if(int.Parse(splitMoveset[i]) <= lv){
				//if the moveset has less than 7 moves.
				if(mS.Length<21){
					mS += splitMoveset[i-1];
				}
			}
		}

		//now the mS variable contains the latest 7 (or less) moves the monster has learned
		return mS;
	}

	public float IndexToGrowthRate(string id){
		float gr=1.0f;

		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);

		for(int i=0;i<nodes.Count;i++){
			if(nodes[i].ChildNodes[0].InnerText == id){
				gr=float.Parse(nodes[i].ChildNodes[14].InnerText);
			}
		}

		return gr;

	}

	public float InstanceToGrowthRate(int id){
		return growthRate[id];
	}

	public string IndexToFollowType(int id){
		string fT="";

		string levelsxpath = "/monsters/monster";
		XmlNodeList nodes = xmlDoc.SelectNodes(levelsxpath);

		for(int i=0;i<nodes.Count;i++){
			if(nodes[i].ChildNodes[0].InnerText == id.ToString()){
				fT=nodes[i].ChildNodes[15].InnerText;
			}
		}

		return fT;
	}

	public string InstanceToFollowType(int id){
		return followType[id];
	}

	public void HealTeam(){
		for(int i=0;i<party.Length;i++){
 			currentHP[party[i]] = hp[party[i]];
		}
	}

	
	public string[] GenerateEnemy(int id, int lvl){

		string[] enemy = new string[24];

		//fill in basic data
		enemy[0] = id.ToString ();
		enemy[1] = lvl.ToString ();

		//get modifiers
		double h= (Random.Range (0.6f,1.4f)*IndexToHP(id));
		double a= (Random.Range (0.6f,1.4f)*IndexToAtk(id));
		double m= (Random.Range (0.6f,1.4f)*IndexToMag(id));
		double s= (Random.Range (0.6f,1.4f)*IndexToSpd(id));
		double r= (Random.Range (0.6f,1.4f)*IndexToRes(id));
		double d= (Random.Range (0.6f,1.4f)*IndexToDef(id));

		//get stats
		enemy[2] = ((lvl*3f)*h).ToString();
		enemy[3] = ((lvl)*a).ToString();
		enemy[4] = ((lvl)*m).ToString();
		enemy[5] = ((lvl)*s).ToString();
		enemy[6] = ((lvl)*r).ToString();
		enemy[7] = ((lvl)*d).ToString();
		enemy[8] = "0";
		enemy[9] = IndexToMoveset(lvl,IndexToLearnSet(id.ToString()));
		enemy[10] = id.ToString();
		enemy[11] = IndexToPaletteID(id).ToString();
		enemy[12] = IndexToAttr(id);
		enemy[13] = IndexToName(id);
		enemy[14] = IndexToLearnSet(id.ToString());
		enemy[15] = IndexToQuirk(id.ToString());
		enemy[16] = IndexToGrowthRate(id.ToString()).ToString();
		enemy[17] = h.ToString();
		enemy[18] = a.ToString();
		enemy[19] = m.ToString();
		enemy[20] = s.ToString();
		enemy[21] = r.ToString();
		enemy[22] = d.ToString();
		enemy[23] = IndexToFollowType(id);

		return enemy;
	

	}

	public void AddMonster(string[] monster){
		ExpandArrays();

		list[list.Length-1] = monster[0];
		level[list.Length-1]= int.Parse(monster[1]);
		hp[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[2]));
		currentHP[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[2]));//hp variable
		currentStatus[list.Length-1] = "";
		atk[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[3]));
		mag[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[4]));
		spd[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[5]));
		res[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[6]));
		def[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[7]));
		exp[list.Length-1] = Mathf.FloorToInt(float.Parse(monster[8]));
		attacks[list.Length-1] = monster[9];
		sprites[list.Length-1] = int.Parse (monster[10]);
		palettes[list.Length-1] = int.Parse(monster[11]);
		attr[list.Length-1] = monster[12];
		nicknames[list.Length-1] = monster[13];
		learnSet[list.Length-1] = monster[14];
		quirk[list.Length-1] = monster[15];
		growthRate[list.Length-1] = float.Parse(monster[16]);
		hpSeed[list.Length-1] = double.Parse(monster[17]);
		atkSeed[list.Length-1] = double.Parse(monster[18]);
		magSeed[list.Length-1] = double.Parse(monster[19]);
		spdSeed[list.Length-1] = double.Parse(monster[20]);
		resSeed[list.Length-1] = double.Parse(monster[21]);
		defSeed[list.Length-1] = double.Parse(monster[22]);
		followType[list.Length-1] = monster[23];

		Debug.Log("Monster added!");

		//if party is empty add to.
		if(party.Length<10){
			int[] temp = new int[party.Length+1];
			party.CopyTo(temp,0);
			party = temp;
			party[party.Length-1] = list.Length-1;
		}
	}

	public void ResetMonsters(){

		list = new string[0];
		level = new int[0];
		hp = new int[0];
		currentHP = new int[0];
		currentStatus = new string[0];
		atk = new float[0];
		mag = new float[0];
		spd = new float[0];
		res = new float[0];
		def = new float[0];
		exp = new double[0];
		attacks = new string[0];
		sprites = new int[0];
		palettes = new int[0];
		attr = new string[0];
		nicknames = new string[0];
		learnSet = new string[0];
		quirk = new string[0];
		growthRate = new float[0];
		hpSeed = new double[0];
		atkSeed = new double[0];
		magSeed = new double[0];
		spdSeed = new double[0];
		resSeed = new double[0];
		defSeed = new double[0];
		followType = new string[0];

	}

	void ExpandArrays(){
		//list
		string[] temp0 = new string[list.Length+1];
		list.CopyTo(temp0,0);
		list = temp0;

		//level
		int[] temp1 = new int[list.Length+1];
		level.CopyTo(temp1,0);
		level = temp1;

		//hp
		int[] temp2 = new int[list.Length+1];
		hp.CopyTo(temp2,0);
		hp = temp2;
		int[] t = new int[list.Length+1];
		currentHP.CopyTo(t,0);
		currentHP = t;
		string[] s = new string[list.Length+1];
		currentStatus.CopyTo(s,0);
		currentStatus = s;

		//atk
		float[] temp3 = new float[list.Length+1];
		atk.CopyTo(temp3,0);
		atk = temp3;

		//mag
		float[] temp4 = new float[list.Length+1];
		mag.CopyTo(temp4,0);
		mag = temp4;

		//spd
		float[] temp5 = new float[list.Length+1];
		spd.CopyTo(temp5,0);
		spd = temp5;

		//res
		float[] temp6 = new float[list.Length+1];
		res.CopyTo(temp6,0);
		res = temp6;

		//def
		float[] temp7 = new float[list.Length+1];
		def.CopyTo(temp7,0);
		def = temp7;

		//exp
		double[] temp8 = new double[list.Length+1];
		exp.CopyTo(temp8,0);
		exp = temp8;

		//attacks
		string[] temp9 = new string[list.Length+1];
		attacks.CopyTo(temp9,0);
		attacks = temp9;

		//sprites
		int[] temp10 = new int[list.Length+1];
		sprites.CopyTo(temp10,0);
		sprites = temp10;

		//palettes
		int[] temp11 = new int[list.Length+1];
		palettes.CopyTo(temp11,0);
		palettes = temp11;

		//attr
		string[] temp12 = new string[list.Length+1];
		attr.CopyTo(temp12,0);
		attr = temp12;

		//nicknames
		string[] temp13 = new string[list.Length+1];
		nicknames.CopyTo(temp13,0);
		nicknames = temp13;

		//learnset
		string[] temp14 = new string[list.Length+1];
		learnSet.CopyTo(temp14,0);
		learnSet = temp14;

		//quirk 
		string[] temp15 = new string[list.Length+1];
		quirk.CopyTo(temp15,0);
		quirk = temp15;

		//growthRate
		float[] temp16 = new float[list.Length+1];
		growthRate.CopyTo(temp16,0);
		growthRate = temp16;

		//hpSeed
		double[] temp17 = new double[list.Length+1];
		hpSeed.CopyTo(temp17,0);
		hpSeed = temp17;

		//atkSeed
		double[] temp18 = new double[list.Length+1];
		atkSeed.CopyTo(temp18,0);
		atkSeed = temp18;

		//magSeed
		double[] temp19 = new double[list.Length+1];
		magSeed.CopyTo(temp19,0);
		magSeed = temp19;

		//spdSeed
		double[] temp20 = new double[list.Length+1];
		spdSeed.CopyTo(temp20,0);
		spdSeed = temp20;

		//resSeed
		double[] temp21 = new double[list.Length+1];
		resSeed.CopyTo(temp21,0);
		resSeed = temp21;

		//defSeed
		double[] temp22 = new double[list.Length+1];
		defSeed.CopyTo(temp22,0);
		defSeed = temp22;

		//followType
		string[] temp23 = new string[list.Length+1];
		followType.CopyTo(temp23,0);
		followType= temp23;

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.P)){
			AddMonster(GenerateEnemy(0,5));
		}
	}

}

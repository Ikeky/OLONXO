using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
	[Header("Статистика ружья")]
	//"Rifle", true, 8, 30, 5f, s_anime, s_obj, s_dulo, 8, 0.8f, 70, 5f, RifleAudios
	public string r_name = "rifle";
	public int r_ammo = 8;
	public int r_magazin = 30;
	public float r_sensetivity = 5f;
	public int r_ammoempty = 8;
	public float r_firerate = 0.8f;
	public int r_damage = 70;

	[Header("Rifle")]
    public Rigidbody bullet;
	public AudioClip[] RifleAudios;
    public Animator s_anime;
    public GameObject s_obj;
    public GameObject s_dulo;
    [Header("HUD")]
    public Text Ammonition;
	Transform Camera;
    float timefire = 0f;
    int recoildown;
    int recoilright;
    float timer = 0f;

    public Gun[] guns = new Gun[2];
    public class Gun{
		public string name { get; set; }
        public bool enabled = false;
		public GameObject obj { get; set; }
		public GameObject dulo { get; set; }
		public Animator anime { get; set; }
		public int ammo { get; set; }
		public int magazin { get; set; }
        public float Mouse_Sens = 5f;
		public int ammoempty { get; set; }
		public int minrange { get; set; }
		public int maxrange { get; set; }
		public int Damage { get; set; }
		public float firerate { get; set; }
		public float timefire { get; set; }
		public float dist { get; set; }
		public AudioClip[] audio { get; set; }
        public void Fire(int Irecoilright, int Irecoildown, Transform ICamera, Rigidbody Iobjbul, int _damage, Vector3 p_tr)
        {
			Quaternion bulrotX = Quaternion.AngleAxis (1,Vector3.up+new Vector3(0,Irecoilright,0));
			Quaternion bulrotY = Quaternion.AngleAxis (1,Vector3.right+new Vector3(0,Irecoildown,0));
			Quaternion bulrotZ = Quaternion.AngleAxis (-135,Vector3.forward);
			Rigidbody bullet = Instantiate (Iobjbul, dulo.GetComponent<Transform>().position, ICamera.rotation * bulrotY * bulrotX * bulrotZ);
			bullet.AddForce (bullet.GetComponent<Transform>().forward * 7000f);
			bullet.GetComponent<BulletChecker>().Damage = _damage;
			bullet.GetComponent<BulletChecker>().p_tr = p_tr;
            ammo--;
        }
        public void SoundReload(AudioSource ass)
        {
           if (enabled)
           {
				ass.clip = audio[0];
				ass.Play();
           }
        }
		public void Reload(AudioSource ass){
                    if (ammo != ammoempty && magazin != 0)
                    {
                        if ((magazin / ammoempty).ToString("0") != "0")
                        {
                            magazin -= ammoempty - ammo;
                            ammo += ammoempty - ammo;
							SoundReload(ass);
                        }
                        else if ((magazin / ammoempty).ToString("0") == "0" && ammo + magazin <= ammoempty)
                        {
                            ammo += magazin;
                            magazin = 0;
							SoundReload(ass);

                        }
                        else
                        {
                            ammo += magazin;
                            magazin -= magazin;
							SoundReload(ass);
                        }
                    }
		}
		public Gun(string aname, bool aenabled, int aammo, int amagazin, float asense, Animator aanime, GameObject aobj, GameObject adulo, int aammoempty, float afirerate, int adamage, float adist,AudioClip[] aaudio)
        {
			audio = aaudio;
            name = aname;
            aammo = aammo;
            enabled = aenabled;
            magazin = amagazin;
            Mouse_Sens = asense;
            anime = aanime;
            dulo = adulo;
            obj = aobj;
            ammoempty = aammoempty;
            firerate = afirerate;
            Damage = adamage;
            dist = adist;
		}
	}
    void Start()
    {
        Camera = gameObject.transform.GetChild(0).GetComponent<Transform>();
		guns[0] = new Gun(r_name, true, r_ammo, r_magazin, r_sensetivity, s_anime, s_obj, s_dulo, r_ammoempty, r_firerate, r_damage, 5f, RifleAudios);
    }
	void FixedUpdate(){
		float rbm = Input.GetAxis("Fire2");
		foreach (Gun guner in guns) {
			if (guner != null && guner.enabled) {
				if(rbm > 0.1f){
					guner.anime.SetBool ("Is_Aiming", true);
				}
				if (rbm < 0.1f){
					guner.anime.SetBool ("Is_Aiming", false);
				}
			}
		}
        foreach(Gun guner in guns)
        {
            if (guner != null && guner.enabled)
            {
                Ammonition.text = guner.ammo + "/" + guner.magazin;
                timer += Time.deltaTime;
                if (Input.GetButton("Fire1") && timer >= guner.firerate && Time.timeScale != 0 && guner.ammo > 0)
				{
					timer = 0f;
                    timefire = Time.time + 1f / guner.firerate;
                    recoildown = Random.Range(-4, 3);
					recoilright = Random.Range(-5, 5);
					guner.Fire(recoilright, recoildown, Camera, bullet, guner.Damage, gameObject.transform.position);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
					guner.Reload(gameObject.GetComponent<AudioSource>());
                }
            }
        }
    }
}

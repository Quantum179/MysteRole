using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mysterole;
using System.Collections.Generic;
using System;

public class DebugLstMonstres : MonoBehaviour {

    public Dropdown ListeMonstres;
    public GameObject MonstreA;
    public GameObject MonstreB;
    public GameObject MonstreC;
    public GameObject MonstreD;

    private Dropdown ListeMonstreA;
    private Dropdown ListeMonstreB;
    private Dropdown ListeMonstreC;
    private Dropdown ListeMonstreD;

    private InputField NivMonstreA;
    private InputField NivMonstreB;
    private InputField NivMonstreC;
    private InputField NivMonstreD;

    private int _niveau1 { get { return int.Parse(NivMonstreA.text); } }
    private int _niveau2 { get { return int.Parse(NivMonstreB.text); } }
    private int _niveau3 { get { return int.Parse(NivMonstreC.text); } }
    private int _niveau4 { get { return int.Parse(NivMonstreD.text); } }

    private List<int> RolesMonstres = new List<int>();

    private static DebugLstMonstres _moi;

    public static uint Niveau1 { get { return (uint)_moi._niveau1; } }
    public static uint Niveau2 { get { return (uint)_moi._niveau2; } }
    public static uint Niveau3 { get { return (uint)_moi._niveau3; } }
    public static uint Niveau4 { get { return (uint)_moi._niveau4; } }

    public static int Monstre1
    {
        get
        {
            int id = int.Parse(_moi.ListeMonstreA.options[_moi.ListeMonstreA.value].text.Split(' ')[0]);
            return id;
        }
    }

    public static int Monstre2
    {
        get
        {
            int id;
            try
            {
                id = int.Parse(_moi.ListeMonstreB.options[_moi.ListeMonstreB.value].text.Split(' ')[0]);
            }
            catch
            {
                id = -1;
            }
            return id;
        }
    }

    public static int Monstre3
    {
        get
        {
            int id;
            try
            {
                id = int.Parse(_moi.ListeMonstreC.options[_moi.ListeMonstreC.value].text.Split(' ')[0]);
            }
            catch
            {
                id = -1;
            }
            return id;
        }
    }

    public static int Monstre4
    {
        get
        {
            int id;
            try
            {
                id = int.Parse(_moi.ListeMonstreD.options[_moi.ListeMonstreD.value].text.Split(' ')[0]);
            }
            catch
            {
                id = -1;
            }
            return id;
        }
    }

    public static Dropdown Liste { get { return _moi.ListeMonstres; } }

    void Awake()
    {
        ListeMonstreA = MonstreA.GetComponentInChildren<Dropdown>();
        NivMonstreA = MonstreA.GetComponentInChildren<InputField>();
        NivMonstreA.text = "1";

        ListeMonstreB = MonstreB.GetComponentInChildren<Dropdown>();
        NivMonstreB = MonstreB.GetComponentInChildren<InputField>();
        NivMonstreB.text = "1";

        ListeMonstreC = MonstreC.GetComponentInChildren<Dropdown>();
        NivMonstreC = MonstreC.GetComponentInChildren<InputField>();
        NivMonstreC.text = "1";

        ListeMonstreD = MonstreD.GetComponentInChildren<Dropdown>();
        NivMonstreD = MonstreD.GetComponentInChildren<InputField>();
        NivMonstreD.text = "1";
        _moi = this;
    }

    // Use this for initialization
    void Start () {
        Dictionary<int, Equipe>.Enumerator e = DonneesJeu.EquipesMonstre.GetEnumerator();
        Dropdown.OptionData odman = new Dropdown.OptionData(" - Manuel - ");
        ListeMonstres.options.Add(odman);

        while (e.MoveNext())
        {
            Dropdown.OptionData od = new Dropdown.OptionData(e.Current.Key + " - " + e.Current.Value.Nom);
            ListeMonstres.options.Add(od);
        }

        odman = new Dropdown.OptionData(" - Aucun - ");
        ListeMonstreB.options.Add(odman);
        ListeMonstreC.options.Add(odman);
        ListeMonstreD.options.Add(odman);

        //Dictionary<int, Role>.Enumerator e2 = DonneesJeu.Monstres.GetEnumerator();

        Dictionary<int, Role>.Enumerator em = DonneesJeu.Monstres.GetEnumerator();

        while(em.MoveNext())
        {
            Dropdown.OptionData od = new Dropdown.OptionData(em.Current.Key + " - " + em.Current.Value.Nom);
            ListeMonstreA.options.Add(od);
            ListeMonstreB.options.Add(od);
            ListeMonstreC.options.Add(od);
            ListeMonstreD.options.Add(od);
            RolesMonstres.Add(em.Current.Key);
        }

        ListeMonstres.RefreshShownValue();
        ListeMonstreA.RefreshShownValue();
        ListeMonstreB.RefreshShownValue();
        ListeMonstreC.RefreshShownValue();
        ListeMonstreD.RefreshShownValue();
        ListeMonstres.value = 0;
    }

    // Update is called once per frame
    void Update() {
        if (ListeMonstres.value != 0)
        {
            Equipe E = DonneesJeu.EquipesMonstre[ListeMonstres.value - 1];
            ListeMonstreA.value = RolesMonstres.IndexOf(E.Membres[0].Role.ID);
            NivMonstreA.text = E.Membres[0].NIV.ToString();
            if (E.Membres.Count >= 2)
            {
                ListeMonstreB.value = RolesMonstres.IndexOf(E.Membres[1].Role.ID) + 1;
                NivMonstreB.text = E.Membres[1].NIV.ToString();
            }
            else
            {
                ListeMonstreB.value = 0;
                NivMonstreB.text = "1";
            }
            if (E.Membres.Count >= 3)
            {
                ListeMonstreC.value = RolesMonstres.IndexOf(E.Membres[2].Role.ID) + 1;
                NivMonstreC.text = E.Membres[2].NIV.ToString();
            }
            else
            {
                ListeMonstreC.value = 0;
                NivMonstreC.text = "1";
            }
            if (E.Membres.Count == 4)
            {
                ListeMonstreD.value = RolesMonstres.IndexOf(E.Membres[3].Role.ID) + 1;
                NivMonstreD.text = E.Membres[3].NIV.ToString();
            }
            else
            {
                ListeMonstreD.value = 0;
                NivMonstreD.text = "1";
            }

            DeactiverListesM();
        }
        else
        {
            ActiverListesM();
        }
    }

    private void DeactiverListesM()
    {
        ListeMonstreA.interactable = false;
        ListeMonstreB.interactable = false;
        ListeMonstreC.interactable = false;
        ListeMonstreD.interactable = false;
        NivMonstreA.interactable = false;
        NivMonstreB.interactable = false;
        NivMonstreC.interactable = false;
        NivMonstreD.interactable = false;
    }

    private void ActiverListesM()
    {
        ListeMonstreA.interactable = true;
        ListeMonstreB.interactable = true;
        ListeMonstreC.interactable = true;
        ListeMonstreD.interactable = true;
        NivMonstreA.interactable = true;
        NivMonstreB.interactable = true;
        NivMonstreC.interactable = true;
        NivMonstreD.interactable = true;
    }
}

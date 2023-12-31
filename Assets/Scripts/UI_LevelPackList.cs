using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;

    [SerializeField]
    private RectTransform _content = null;

    private void Start()
    {
        //LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(null, _inisialData.levelPack, false);
        }

        // Subscribe events
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy()
    {
        // Unsubscribe events
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci)
            return;
            
        // Buka Menu levels
        // _levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        // Tutup Menu level packs
        // gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;
        _animator.SetTrigger("KeLevels");
    }

    public void LoadLevelPack(LevelPackKuis[] levelPacks, PlayerProgress.MainData playerData)
    {
        foreach (var lp in levelPacks)
        {
            // Membuat salinan objek dari prefab tombol level pack
            var t = Instantiate(_tombolLevelPack);

            t.SetLevelPack(lp);

            // Masukkan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            // Cek apakah level pack terdaftar di dictionary progress pemain
            if (!playerData.progressLevel.ContainsKey(lp.name))
            {
                // Jika tidak terdaftar maka level Pack terkunci
                t.KunciLevelPack();
            }
        }
    }

    internal void LoadLevelPack(object levelPacks, PlayerProgress.MainData progressData)
    {
        throw new NotImplementedException();
    }
}

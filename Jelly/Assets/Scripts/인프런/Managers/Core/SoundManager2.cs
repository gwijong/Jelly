using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];//MaxCount 는 항상 열거형 맨 끝에 쓰세요
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };  //없으면 생성
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play( string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
     
        if(audioClip == null)
        {
            return;
        }

        if (type == Define.Sound.Bgm)
        {       
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];  //배경음

            if (audioSource.isPlaying)  //배경음이 이미 실행중인 경우
            {
                audioSource.Stop();  //기존 배경음 중지
            }
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];  //이펙트는
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip); // 한번 실행
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)  //사운드 폴더 경로로 강제로 맞춰줌
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioClip = null; //초기화식

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);  //path를 통해 오디오클립을 찾아 대입
           

        }
        else  //이펙트일 경우
        {
            
            if (_audioClips.TryGetValue(path, out audioClip) == false)//딕셔너리에 등록되어있는지 체크
            {
                audioClip = Managers.Resource.Load<AudioClip>(path); //없었으면 로드
                _audioClips.Add(path, audioClip); // 딕셔너리에도 넣어주기
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }
}

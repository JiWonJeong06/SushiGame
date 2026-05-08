using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string jp;
    public string kr;
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueLine> gameOver;
    public List<DialogueLine> gameClear;
    public List<DialogueLine> eatSushi;
}

public  class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("JSON 파일")]
    public TextAsset dialogueJSON;

    [Header("UI")]
    public TextMeshProUGUI dialogueText;

    [Header("Audio")]
    public AudioSource voiceSource;
    public AudioClip[] gameOverVoices;   // 5개 순서대로
    public AudioClip[] gameClearVoices;  // 5개 순서대로
    public AudioClip[] eatSushiVoices;   // 5개 순서대로

    private DialogueData dialogueData;

   


    void Start()
    {
        // JSON 파싱
        dialogueData = JsonUtility.FromJson<DialogueData>(dialogueJSON.text);
    }

    // 랜덤 대사 출력 + 음성 재생
    void PlayDialogue(List<DialogueLine> lines, AudioClip[] voices)
    {
        int index = Random.Range(0, lines.Count);
        DialogueLine line = lines[index];

        // 한국어 텍스트 표시
        dialogueText.text = line.kr;

        // 일본어 음성 재생
        if (voices != null && index < voices.Length && voices[index] != null)
        {
            voiceSource.PlayOneShot(voices[index]);
        }
    }

    // 게임 오버 대사
    public void PlayGameOver()
    {
        PlayDialogue(dialogueData.gameOver, gameOverVoices);
    }

    // 게임 클리어 대사
    public void PlayGameClear()
    {
        PlayDialogue(dialogueData.gameClear, gameClearVoices);
    }

    // 초밥 먹을 때 대사
    public void PlayEatSushi()
    {
        PlayDialogue(dialogueData.eatSushi, eatSushiVoices);
    }

    // 대사 숨기기
    public void HideDialogue()
    {
        dialogueText.text = "";
    }
}
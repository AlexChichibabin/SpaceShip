using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShip
{
    public class TotalStatisticsPanel : SingletonBase<TotalStatisticsPanel>
    {
        [SerializeField] private Text m_TotalKills;
        [SerializeField] private Text m_TotalScores;
        [SerializeField] private Text m_TotalTime;
        [SerializeField] private Button m_ResetButton;

        private void Start()
        {
            m_TotalKills.text = "Total Kills : " + TotalStatistics.Instance.TotalNumKills.ToString();
            m_TotalScores.text = "Total Scores : " + TotalStatistics.Instance.TotalScores.ToString();
            m_TotalTime.text = "Total Time : " + TotalStatistics.Instance.TotalTime.ToString();

            m_ResetButton.onClick.AddListener(OnButtonReset);
            TotalStatistics.Instance.UpdateTotalStatistics.AddListener(OnUpdateTotalStatistics);
        }

        public void OnButtonBackFromStatisticsPanel()
        {
            MainMenuController.Instance.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnButtonReset()
        {
            TotalStatistics.Instance.OnButtonReset();
        }

            private void OnUpdateTotalStatistics()
        {
            m_TotalKills.text = "Total Kills : " + TotalStatistics.Instance.TotalNumKills.ToString();
            m_TotalScores.text = "Total Scores : " + TotalStatistics.Instance.TotalScores.ToString();
            m_TotalTime.text = "Total Time : " + TotalStatistics.Instance.TotalTime.ToString();
        }
    }
}
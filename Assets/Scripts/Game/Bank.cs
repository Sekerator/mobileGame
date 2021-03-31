﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
	public GameObject thisPanel, mainPanel;
	private GlobalParam globalParametrs;

	// объекты для взятия кредита
	public GameObject infoTakeCreditPanel;
	public Text	percentCreditText, periodCredit, sumAmount;
	public InputField sumAmountCredit;
	
	// объекты для погашения кредита
	public GameObject infoRepayCredirPanel;
	public Text	balanceCredit, periodCreditText;
	public InputField sumAmountFieldR;

	public Text sumCreditGeneral, periodCreditGenerel;

	private void Awake()
	{
		globalParametrs = mainPanel.GetComponent<GlobalParam>();
		sumCreditGeneral.text = "0";
		periodCreditGenerel.text = "0";
	}
	public void closeButt()
	{
		thisPanel.SetActive(false);
	}
	public void closeButtT()
	{
		infoTakeCreditPanel.SetActive(false);
	}
	public void closeButtR()
	{
		infoRepayCredirPanel.SetActive(false);
	}
	// открытие панели "Взять кредит"
	public void take_butt()
	{
		infoTakeCreditPanel.SetActive(true);
		
	}
	// открытие панели "Погасить кредит"
	public void repay_butt()
	{
		infoRepayCredirPanel.SetActive(true);
		//остаток по кредиту
		balanceCredit.text = sumCreditGeneral.text + '$';
		// вычисление процента кредита
		periodCreditText.text = periodCreditGenerel.text;
		
	}
	// нажатие кнопки "Взять кредит"
	public void takeCredit_butt()
	{
		sumCreditGeneral.text = (Convert.ToInt32(sumCreditGeneral.text) + Convert.ToInt32(sumAmountCredit.text)).ToString();
		
		globalParametrs.money.text =
			(Convert.ToInt32(globalParametrs.money.text) + Convert.ToInt32(sumAmountCredit.text)).ToString();
		// вычисление процента кредита
		if (Convert.ToInt32(sumAmountCredit.text) <= 10000)
			percentCreditText.text = "10 %";
		if ((Convert.ToInt32(sumAmountCredit.text) > 10000) && (Convert.ToInt32(sumAmountCredit.text) <= 50000))
			percentCreditText.text = "15 %";
		if (Convert.ToInt32(sumAmountCredit.text) > 50000)
			percentCreditText.text = "25 %";

		// вычисление срока кредита
		if (Convert.ToInt32(sumAmountCredit.text) <= 10000)
		{
			periodCredit.text = "1 года";
			periodCreditGenerel.text = periodCredit.text;
		}
		if ((Convert.ToInt32(sumAmountCredit.text) > 10000) && (Convert.ToInt32(sumAmountCredit.text) <= 50000))
		{
			periodCredit.text = "3 лет";
			periodCreditGenerel.text = periodCredit.text;
		}
		if (Convert.ToInt32(sumAmountCredit.text) > 50000)
		{
			periodCredit.text = "5 лет";
			periodCreditGenerel.text = periodCredit.text;
		}

		// вычисление суммы выплаты в педиод
		if (Convert.ToInt32(sumAmountCredit.text) <= 10000)
			sumAmount.text = ((Convert.ToDouble(sumAmountCredit.text) +
			                   Convert.ToDouble(sumAmountCredit.text) * 0.1) / 4).ToString() + '$';
		if ((Convert.ToInt32(sumAmountCredit.text) > 10000) && (Convert.ToInt32(sumAmountCredit.text) <= 50000))
			sumAmount.text = ((Convert.ToDouble(sumAmountCredit.text) + 
			                   Convert.ToDouble(sumAmountCredit.text) * 0.15) / 12).ToString() + '$';
		if (Convert.ToInt32(sumAmountCredit.text) > 50000)
			sumAmount.text = ((Convert.ToDouble(sumAmountCredit.text) + 
			                   Convert.ToDouble(sumAmountCredit.text) * 0.25) / 20).ToString() + '$';
	}
	// нажатие кнопки "Погасить кредит"
	public void repayCredit_butt()
	{
		//оснонуление глобальной переменной по сумме кредита и по периоду
		if (Convert.ToInt32(sumAmountFieldR.text) == Convert.ToInt32(sumCreditGeneral.text))
		{
			globalParametrs.money.text =
				(Convert.ToInt32(globalParametrs.money.text) - Convert.ToInt32(sumAmountFieldR.text)).ToString();

			balanceCredit.text = "";
			periodCreditText.text = "";
			
			sumCreditGeneral.text = "0";
			periodCreditGenerel.text = "0";
		}
	}

}

using System;
using System.Collections.Generic;
using System.Globalization;

namespace NacLifeStmtCleaner {
    internal class Entry {
        private string name;
        private string policyNum;
        private double rate;
        private List<CommLine> commLines;
        private List<AnnualLine> annualLines;
        private TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public Entry(string name, string policyNum, double rate) {
            this.name = name;
            this.policyNum = policyNum;
            commLines = new List<CommLine>();
            annualLines = new List<AnnualLine>();
            this.rate = rate;
        }

        public double getCommissionTotal() {
            double total = 0.0;
            double commissions = 0.0;
            double commApps = 0.0;
            double cBacks = 0.0;
            double advs = 0.0;

            foreach (CommLine cl in commLines) {
                commissions += cl.comm;
            }

            foreach (AnnualLine al in annualLines) {
                commApps += al.commApp;
                cBacks += al.chargeBack;
                advs += al.currAdv;
            }

            total = commissions - commApps + cBacks + advs;
            return total;
        }

        public string printOut() {
            string strOut = "";

            strOut = policyNum + ", " + name + ", " + commLines[0].plan + ", " + commLines[0].accDate + ", " +
            commLines[0].premium + ", " + rate + ", ";

            if (commLines[0].split < 0.1) {
                strOut += "100,";
            }
            else {
                strOut += (commLines[0].split * 100) + ", ";
            }

            if (commLines[0].type == "RN") {
                strOut += "0, " + getCommissionTotal();
            }
            else {
                strOut += getCommissionTotal() + ", 0";
            }
            Console.WriteLine(strOut);
            return strOut;
        }

        public Object[] getOutput() {
            Object[] ret = new Object[9];
            ret[0] = policyNum;
            ret[1] = textInfo.ToTitleCase(name.ToLower());
            ret[2] = commLines[0].plan;
            ret[3] = commLines[0].accDate;
            ret[4] = commLines[0].premium;
            ret[5] = commLines[0].cRate * 100;
            ret[6] = commLines[0].split;

            if (commLines[0].type == "RN") {
                ret[8] = getCommissionTotal();
                ret[7] = 0.0;
            }
            else {
                ret[7] = getCommissionTotal();
                ret[8] = 0.0;
            }
            return ret;
        }

        public double getPremium() {
            return commLines[0].premium;
        }

        public double getRatePer() {
            return commLines[0].cRate;
        }

        public double getSplit() {
            double tSplit = commLines[0].split;
            if (tSplit == 0.0) {
                return 100.0;
            }
            else return tSplit * 100;
        }

        public string getType() {
            return commLines[0].type;
        }


        /**
         * @return the rate
         */
        public double getRate() {
            return rate * 100;
        }


        /**
         * @param rate the rate to set
         */
        public void setRate(double rate) {
            this.rate = rate;
        }


        public void addCommLine(CommLine cl) {
            commLines.Add(cl);
        }

        public void addAnnualLine(AnnualLine al) {
            annualLines.Add(al);
        }

        public string getPlan() {
            return commLines[0].plan;
        }

        public string getIssueDate() {
            return commLines[0].accDate;
        }


        /**
         * @return the name
         */
        public string getName() {
            return name;
        }

        /**
         * @param name the name to set
         */
        public void setName(string name) {
            this.name = name;
        }

        /**
         * @return the policyNum
         */
        public string getPolicyNum() {
            return policyNum;
        }

        /**
         * @param policyNum the policyNum to set
         */
        public void setPolicyNum(string policyNum) {
            this.policyNum = policyNum;
        }


        /**
         * @return the commLines
         */
        public List<CommLine> getCommLines() {
            return commLines;
        }

        /**
         * @param commLines the commLines to set
         */
        public void setCommLines(List<CommLine> commLines) {
            this.commLines = commLines;
        }

        /**
         * @return the annualLines
         */
        public List<AnnualLine> getAnnualLines() {
            return annualLines;
        }

        /**
         * @param annualLines the annualLines to set
         */
        public void setAnnualLines(List<AnnualLine> annualLines) {
            this.annualLines = annualLines;
        }
    }
}

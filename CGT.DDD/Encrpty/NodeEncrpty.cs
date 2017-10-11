using Microsoft.AspNetCore.NodeServices;
using System;
using System.Threading.Tasks;

namespace CGT.DDD.Encrpty
{
    /// <summary>
    /// 加解密方法，实现DES，RSA，MD5，AES
    /// </summary>
    public class NodeEncrpty
    {
        public INodeServices nodeServices { get; set; }
        #region DES

        public async Task<string> DESencrypt(string data, string key)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "DESencrypt", data, key);
            return result;
        }

        public async Task<string> DESdecrypt(string data, string key)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "DESdecrypt", data, key);
            return result;
        }

        #endregion

        #region MD5

        public async Task<string> MD5encrypt(string data)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "Md5encrypt", data);
            return result;
        }

        #endregion

        #region RSA
        public async Task<string> RSAencrypt(string data)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "RSAencrypt", data, rsa_publickey);
            return result;
        }

        public async Task<string> RSAencrypt(string data, string rsapublickey)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "RSAencrypt", data, reapalrea_publickey);
            return result;
        }
        public async Task<string> RSAdecrypt(string data)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "RSAdecrypt", data, rsa_privatekey);
            return result;
        }
        public async Task<string> RSAdecrypt(string data, string rsaprivatekey)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "RSAdecrypt", data, reapalrea_privatekey);
            return result;
        }
        #endregion

        #region AES

        public async Task<string> AESencrypt(string data, string key)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "AESencrypt", data, key);
            return result;
        }

        public async Task<string> AESdecrypt(string data, string key)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "AESdecrypt", data, key);
            return result;
        }

        /// <summary>
        /// 随机生成16位AESkey
        /// </summary>
        /// <returns></returns>
        public string GenerateAESKey()
        {
            string str = string.Empty;

            Random rnd1 = new Random();
            int r = rnd1.Next(10, 100);

            long num2 = DateTime.Now.Ticks + r;

            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> r)));
            for (int i = 0; i < 16; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
        #endregion

        #region REM
        public async Task<string> REMencrypt(string data, string keypath)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "RSAencryptCER", data, keypath);
            return result;
        }

        public async Task<string> REMdencrypt(string data, string keypath)
        {
            var result = await nodeServices.InvokeExportAsync<string>("./Encrpty", "RSAdecryptPFX", data, keypath);
            return result;
        }

        #endregion

        #region 测试加密串
        /// <summary>
        /// 只用于分销平台和对内api
        /// </summary>

        public static readonly string rsa_publickey = new Func<String>(() =>
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test")
            {
                return "-----BEGIN PUBLIC KEY-----\n" +
                 "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlnix2KuaKMyFOj3ZGPYN" +
                 "pRdPEarj6u8zUvy0RPATyPv346+x+9RoqHXH9uBJTsbQ2cBmYi0oEkHsqLTebff6" +
                 "PAvTfHiyBKJe1khmWtbwVJRXfRN/hK3jbobREm/GXoJKV7VZCVL3fkND7Pfhkc9W" +
                 "khH7h0//4dH3vB0ww8o5jGtMn2Rh92eGylyoBd8KcS0+j7mywHe17KS+++MTCkU3" +
                 "Hnh4Xx2We0v3RSb/lC8/SdIe++3TsbPeQ5ICTM2K01oGiqqZGZqD5xLhXWQW5e8I" +
                 "b6oems/iY2t/B6CosDkeHo0b6XBGis7J/ggjz0SJDA1/NbCF/cIANLlk6/Qwqk1S" +
                 "VwIDAQAB\n" +
                 "-----END PUBLIC KEY-----";
            }
            else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Formal")
            {
                return "-----BEGIN CERTIFICATE-----\n" +
                        "MIIDMjCCAh6gAwIBAgIQrFnANiqIyp9CujnxnDoMfTAJBgUrDgMCHQUAMCkxJzAl" +
                        "BgNVBAMeHgBpAG4AdABmAG8AbwAxADEAXwAzADkAOQBfADMANDAeFw0xNjA4MTgx" +
                        "MzEzMzRaFw0zOTEyMzEyMzU5NTlaMCkxJzAlBgNVBAMeHgBpAG4AdABmAG8AbwAx" +
                        "ADEAXwAzADkAOQBfADMANDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEB" +
                        "ALzpAon/8FVGWtVvP67XgqRJeacQ2TdxC7TEiSSIfoemVI7PSWeEc+YDejOR9dT0" +
                        "bpHHiM7g3sxieQ00CT6/k8/jgS0vG+choJ0RuTE7ltXP6GQ1d9LFNbVROVv7oqQc" +
                        "ZBScwJz+p7WRQtMPJgLFww/z8iu2X6f3QT0m0vNeVmXaTtk6uBISUm/jtl/p3QZs" +
                        "hNg+lKhCy3p0L5vHv6f6TxuoPS0RUchZ+v87FiF795FknbezqE92RTniAlP/fJ4Y" +
                        "Lh3VjshmawmEi0724YWp8r5JOIfaMJx6BxcwzOjh9S7Wi58wFOMutKw4Rk1QKpre" +
                        "o+fHbidvAGQANLDu65d56D0CAwEAAaNeMFwwWgYDVR0BBFMwUYAQC1fPqsgiwOvS" +
                        "PtF8X3J14qErMCkxJzAlBgNVBAMeHgBpAG4AdABmAG8AbwAxADEAXwAzADkAOQBf" +
                        "ADMANIIQrFnANiqIyp9CujnxnDoMfTAJBgUrDgMCHQUAA4IBAQA2QPJ6nRfrWj0/" +
                        "DPmWH5T+VvtUQ3i3hXvnGBrNzU8SLeMWAGL16pQHOFnZF2mCeM4jIAtxARiN08zZ" +
                        "AW+JqGKhNBvw1IXvISxDtreNZyonI886pDGwyG2Z2miT2IAZlRfv6+AHd6Y5VMhF" +
                        "NAN/MuGFZ+W4CmFqr9baWzW4gfmxylaT49szAYOiGD3LEiX4KnEDSgIZ0DQIIoC7" +
                        "CTclC55pdMtLbd3cl8/H2oNnjoGyAnKRN1jgvKPxGMje5D5hmCMfPwgn7G0naah/" +
                        "MA8iF9YdZBA1/sGKyQQjv29H41LePu3fdRNtkyIqIc7deDxIrYP7Xr4r8mrMpfLc" +
                        "w/TSdto9\n" +
                        "-----END CERTIFICATE-----";
            }
            return null;
        })();

        public static readonly string rsa_privatekey = new Func<String>(() =>
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test")
            {
                return "-----BEGIN RSA PRIVATE KEY-----\n" +
               "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCWeLHYq5oozIU6" +
               "PdkY9g2lF08RquPq7zNS/LRE8BPI+/fjr7H71Giodcf24ElOxtDZwGZiLSgSQeyo" +
               "tN5t9/o8C9N8eLIEol7WSGZa1vBUlFd9E3+EreNuhtESb8ZegkpXtVkJUvd+Q0Ps" +
               "9+GRz1aSEfuHT//h0fe8HTDDyjmMa0yfZGH3Z4bKXKgF3wpxLT6PubLAd7XspL77" +
               "4xMKRTceeHhfHZZ7S/dFJv+ULz9J0h777dOxs95DkgJMzYrTWgaKqpkZmoPnEuFd" +
               "ZBbl7whvqh6az+Jja38HoKiwOR4ejRvpcEaKzsn+CCPPRIkMDX81sIX9wgA0uWTr" +
               "9DCqTVJXAgMBAAECggEAHjCXFi7P3lGyhU31aeGsOSxlC9lqoy9c79CN5qr4fUG4" +
               "qbENl8Y1PV9sQGcdrjWLn7TPlfETch5R+kNXwV6YkUWaKAYbSXy+ZGXgS2eGjqG4" +
               "r/qigh9VW06nUfilJz8i5VgYRhukVmvui/PsCRI9f08mVS7C2y8Ccna02wOO6lIq" +
               "CQxfeORoKRmnusFqUZfFaCcPm57jXACx51jGa3ejsbeLCtnxxaS+c3sXwzQUwOg5" +
               "i/G6gvTyDUiVdfNwZwmhZW8m2M4UX3H6/v87cH0OuqJnocYfCtHYF+8gB7ocDSmv" +
               "bJYQIOiGZp6N6FtnU/twG/JRmb6ancGStP47WRH7gQKBgQC5RAa+mQESfGUkwHWM" +
               "gaInAm5G1RIVsyXM21j2eQLrrykuZtwm9Mr+b4u+q0BfGwAy6yE7F1AsB89XM4h7" +
               "B7ph7RZ4+oxQ30Nc3Gy1+a8AklwXoDzDnMngnZGk2rQEeUTXum5ZFGd4+Q+iGbOy" +
               "Sty8ge9Tc6tGQtSlta36vRfOlwKBgQDP69uh50VRGC1ZK7q8PKOzdmPIOqB2Skb5" +
               "Snt5rwaySI+8jrumoZNKZ+McB4+nI3NQCVZP5FMMx7E/mVv9T1TzbOodg2ak7zHZ" +
               "BRRhEIftFLYfqYeFKlXJwchNveurBD0PZb7NsHyhEuRdhI5LaAa5kEsCSykUKHFE" +
               "LOQtQcbSQQKBgQCmEjelhMv0QTP9RNRaVh3/r+sgbYEfmI5K21kc347hrAtTP/sa" +
               "pJaOi3kOOr6iQjbkbedE6/n+7iVLerNd7aiCfZ+GZLmBCRF+XUzJtv7LmWcZ+pZd" +
               "lt9UDIrw5uc1l2tBoimmimbShxgfa8CsEvVZuXPKL8aZXfD2dix1tilwEwKBgCCO" +
               "mAeUBSCbno418LmE7UUuppU1yOZ83vwuE4OMSookHo2x8+Q1rLCb7831ySKsxGOl" +
               "8qPz5qU7p5+DzgmuIGk1hrs0ViBBbBbFWtmQEximg5YVn6jUZjZ/Z3P05zASK9V7" +
               "YxMmLX2zTZrvJLDeCx+HXpdvWXFUm9fHB7umbxGBAoGABGoSjBndUbSawQZqANjo" +
               "JbiM4cefvhwnqB73mF6P3aTGDJv0oSFLk+WjpcLJi2vsRcyCHZfEoMQxQVehCCN3" +
               "9XzX4bJKFcGsX1AVDROyM0phlXJbeGUlGDWje8iIY0AzZuBpiSHe5M5emUTN2XVS" +
               "Cn4HwiBrTl6mqOKw4Dz1vFM=\n" +
               "-----END RSA PRIVATE KEY-----";
            }
            else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Formal")
            {
                return "-----BEGIN PRIVATE KEY-----\n" +
                        "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC86QKJ//BVRlrV" +
                        "bz+u14KkSXmnENk3cQu0xIkkiH6HplSOz0lnhHPmA3ozkfXU9G6Rx4jO4N7MYnkN" +
                        "NAk+v5PP44EtLxvnIaCdEbkxO5bVz+hkNXfSxTW1UTlb+6KkHGQUnMCc/qe1kULT" +
                        "DyYCxcMP8/Irtl+n90E9JtLzXlZl2k7ZOrgSElJv47Zf6d0GbITYPpSoQst6dC+b" +
                        "x7+n+k8bqD0tEVHIWfr/OxYhe/eRZJ23s6hPdkU54gJT/3yeGC4d1Y7IZmsJhItO" +
                        "9uGFqfK+STiH2jCcegcXMMzo4fUu1oufMBTjLrSsOEZNUCqa3qPnx24nbwBkADSw" +
                        "7uuXeeg9AgMBAAECggEAbrVDJqqWJvNnMiPTN1zXE/53S8Ak/EfEq3huQrm3JToF" +
                        "3fd+tiq6XTb2e5hzcAyba9V6yHqiNzcSZ/lx3ZMaRb+3KUqtWIOtg78qP78DQ2Rn" +
                        "daqcASpPsmXabpyGkRSOVkIS4y6eDFsHxXbgOMm66fN1ncZwQpg1ZGgHQlar3V3T" +
                        "F4MPebYFUQJlN3qcNRNrt9NV4F99iSuEKfjNyxQ2TdbKyxuj6gooj7xKEUZNfmfX" +
                        "FocZrrDlxI+NHNs+BWTsTZ4GfGd9wNjkqvrX2RqV7Bc8H65LAcxBxbnzmdms0Fs8" +
                        "1xlYw6f3zuuwq6q25QKOXat5GZCmBo5NAtvBrgmGAQKBgQDvrpIZ1eKQasLbWDCs" +
                        "wm1U0XC+gQTZsvSxf7BcoYUgEUKEpEq2tfRos5NE5OWCqdzxYGqFBAXf9ajOszWZ" +
                        "BuMFeqyG5BYTrY2sPUDuvTUZUxusQ8WaQL5At+1LNERTQORD+jcUrJdZX+UwdijL" +
                        "XvbejuBWllPWzme/GXisFH8IvQKBgQDJxYk8vAGxWXtHEIyy4JIdkzEB0hM2fTDz" +
                        "jT6I2jnMdwRT5K7No1tE80k1j7CqsJxwgLQcPMO3opWIn4ox3Y/Wald+uH0Xj9tz" +
                        "8xYUIyZ9dSRIEMstFxqv5yrcwBCuM0sdndN39UPSdQinqjFYsLwAxmJB/7NxruU6" +
                        "bZPn4CsVgQKBgQDizC+XntpaiYuE/vhgIUJPZjfnLDKoxTvh0c/Sd0x/QZXN6aCB" +
                        "L7/sazBJmsbKU22sQe3JnDrq54wqu01WVjvv7Vrf8WgqiqlqeDO/NO7P3eJrqV2M" +
                        "TwM4PlKePQECPn9BC72fJIm6mpFGwCht+JhllJEXnfwLYRiura2QN3zViQKBgEhe" +
                        "c3S3Mhw8Lf01MGHQLlgxPPicMovupyoT1QQc0XJGleTJNvFG0CuQ3wc4/HKcWd/4" +
                        "KaGfIcuxotSX9GciQr7y8WLgJO3YiSuzWo3qdZm19EdszpLjoIzK6xK2SMVSXHTI" +
                        "8NorvCG2HF6WqpBXA3d7sC5f2irCQjGMr+oQ+yMBAoGBAI39ZhcHXmaRq6mlbSKL" +
                        "QmUy7/uqUnDt8mXmX4D7ZLTgGuumzcZY3O+2frGKibLx/SlC7mTPct0xBlPEfcNj" +
                        "DBzrXOqZZUmY8AGSdS1DMOR5WHlTvVHnoEuULT09JiU0jPX/KDiDFCB+Ult8OhiR" +
                        "Bpv4iqQFcQxK//LtYQoNHMMj\n" +
                        "-----END PRIVATE KEY-----";
            }
            return null;
        })();

        ///融宝测试证书
        public static readonly string reapalrea_publickey = new Func<String>(() =>
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test")
            {
                return "-----BEGIN PUBLIC KEY-----\n" +
                        "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAudFyFxfFnCCvV2GhbCHE" +
                        "6ZOrX7WG/0d0awdzEwsa+h7hSibJJOJB6SSSVH4Zd/NrvWXr6/1SIc7Lw9xiXFPE" +
                        "HxHf9Z9t24OOTCBQmtkopvwZb8gmwnkMid9EOtkGlgVHDRysMViypT6ddCtJtCJ7" +
                        "FkgHczML90fVyqlJ0ndbZo66WrIRuiS3rEq85P19hFRIjI4MYWVJsJjxdipfCzGY" +
                        "nbeNFruh9EfiLnI+9uk1MZM/uSo8snIXg2PqxJZA/SoIeC8S9t7ZHPJU3eIgveVD" +
                        "jiAsrZd3NFX7Sw0TnexGl/JZ5CCvgUgOrNil83M5B9p9R4bgN0lH233ZEM5ePTib" +
                        "kQIDAQAB\n" +
                        "-----END PUBLIC KEY-----";
            }
            else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Formal")
            {
                return "-----BEGIN CERTIFICATE-----\n" +
                        "MIIEOzCCAyOgAwIBAgIUHznWu9UA1D4ylWrVYDxpmGb2w5owDQYJKoZIhvcNAQEF" +
                        "BQAwezELMAkGA1UEBhMCQ04xEzARBgNVBAoMCmlUcnVzQ2hpbmExHDAaBgNVBAsM" +
                        "E0NoaW5hIFRydXN0IE5ldHdvcmsxOTA3BgNVBAMMMGlUcnVzQ2hpbmEgQ2xhc3Mg" +
                        "MiBFbnRlcnByaXNlIFN1YnNjcmliZXIgQ0EgLSBHMzAeFw0xMzEyMDMwNjUzMzBa" +
                        "Fw0xNDEyMDMwNjUzMzBaMIGtMSAwHgYJKoZIhvcNAQkBFhFpdHJ1czAwMUB0ZXN0" +
                        "LmNvbTEVMBMGA1UECwwMVUlEOml0cnVzMDAxMREwDwYDVQQDDAhpdHJ1czAwMTEk" +
                        "MCIGA1UECwwb5oiY55Wl5ZCI5L2c5LqL5Lia6YOo5rWL6K+VMTkwNwYDVQQKDDDl" +
                        "jJfkuqzlpKnlqIHor5rkv6HnlLXlrZDllYbliqHmnI3liqHmnInpmZDlhazlj7gw" +
                        "ggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC50XIXF8WcIK9XYaFsIcTp" +
                        "k6tftYb/R3RrB3MTCxr6HuFKJskk4kHpJJJUfhl382u9Zevr/VIhzsvD3GJcU8Qf" +
                        "Ed/1n23bg45MIFCa2Sim/BlvyCbCeQyJ30Q62QaWBUcNHKwxWLKlPp10K0m0InsW" +
                        "SAdzMwv3R9XKqUnSd1tmjrpashG6JLesSrzk/X2EVEiMjgxhZUmwmPF2Kl8LMZid" +
                        "t40Wu6H0R+Iucj726TUxkz+5KjyycheDY+rElkD9Kgh4LxL23tkc8lTd4iC95UOO" +
                        "ICytl3c0VftLDROd7EaX8lnkIK+BSA6s2KXzczkH2n1HhuA3SUfbfdkQzl49OJuR" +
                        "AgMBAAGjgYMwgYAwCQYDVR0TBAIwADALBgNVHQ8EBAMCBaAwZgYDVR0fBF8wXTBb" +
                        "oFmgV4ZVaHR0cDovL3RvcGNhLml0cnVzLmNvbS5jbi9wdWJsaWMvaXRydXNjcmw/" +
                        "Q0E9MkMxRDk3Mjg4REUxNEY4NUM2NjQwNjk4RkIyNDczOUFGNDdERkQxQTANBgkq" +
                        "hkiG9w0BAQUFAAOCAQEAg98dhcaki4A/eHJkyXr8eMa9YyfVtxSYP8gaZ8cuHYQU" +
                        "aCmRzeBNUSHAV5mt2rp80Wnc9VXQJk4kwGzoVrfrYpmn7DUH+qAUe2jcxS8tR+OT" +
                        "+iDcYYKofpyKzX6BLXqBspu99KeerJoz6xHJXnsURc04t+1qEYCvgRy2+FB0/Lzu" +
                        "6nouByPmAGQ/Hhlc4h1m4r9kUB2gxEgrD4pDbcsoKG2/2hdWPh50GyvXHnYKz+5m" +
                        "3hPdNYPLFIUSxq9kns8nuU2537TSEik7wXMa7kE8Dg9uxQaPj9pYeTN1E4fS/tdd" +
                        "oplxrKVM2jl9sCrvYmjTEc3YOZWBfx94avcNYEJJGg==\n" +
                        "-----END CERTIFICATE-----";
            }
            return null;
        })();



        public static readonly string reapalrea_privatekey = new Func<String>(() =>
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test")
            {
                return "-----BEGIN RSA PRIVATE KEY-----" +
                        "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC50XIXF8WcIK9X" +
                        "YaFsIcTpk6tftYb/R3RrB3MTCxr6HuFKJskk4kHpJJJUfhl382u9Zevr/VIhzsvD" +
                        "3GJcU8QfEd/1n23bg45MIFCa2Sim/BlvyCbCeQyJ30Q62QaWBUcNHKwxWLKlPp10" +
                        "K0m0InsWSAdzMwv3R9XKqUnSd1tmjrpashG6JLesSrzk/X2EVEiMjgxhZUmwmPF2" +
                        "Kl8LMZidt40Wu6H0R+Iucj726TUxkz+5KjyycheDY+rElkD9Kgh4LxL23tkc8lTd" +
                        "4iC95UOOICytl3c0VftLDROd7EaX8lnkIK+BSA6s2KXzczkH2n1HhuA3SUfbfdkQ" +
                        "zl49OJuRAgMBAAECggEAKYu/w5QIFdFlAteMXP3r5gGjVoHizh7u06NVwlNAU94l" +
                        "pwum+30vGW6Y3RVYom4lj7JDHn+EJpTzEDatM1FzTzMS7PsZd/QhE7pB621UXomN" +
                        "MmYA7IheGlF2EuZFR7OkYDUh9uKLBtFKKRgooYL4HFI8gDBQo5hHH8nb/Je9Mt+i" +
                        "Ow7eZlr2mREUShls8MM9nJRCRKtbxFilOn6v1/J9y/pq6GEX/kbEct5fw00ufMbw" +
                        "j/vusYtiIJeSqZKHX120bXcyngnDw/lIuzl85LJd5v7WyYafQ0BW9fGvwt3Wj6z3" +
                        "hMu5jkcphwWts6enJW7mIUEfLGrjaEUFuEusI70ssQKBgQD5MwPIL48N1iX8b+pb" +
                        "XflF68HV+foeLYNMIJwg7Rz8SrP+DokXizRzfi2PfauvGdCAYZZldpRJzFUvdEzq" +
                        "S1YE///HP6jXLRllS72ZaCdDZdrsaBpyUy3peRhYGMJ7aAq8G9Z9JuqKYx8jE2Eu" +
                        "VaxV18WscXSUHAftKjzi3pRC3QKBgQC+46FVWQN354PkRPcOnTlNcPt5SJbSan7z" +
                        "Lob8gOO/zJ/jwGjnQux+pVEzdDE15wbyldozjw+nUItCYMaNkoTkTG1H4qv035BQ" +
                        "k9ViPbuURMUbOhcZE/f3/kkfKgHq9SQhwtomcBYiZU63hsnSJDYpz1VLgZa3/ZRS" +
                        "Jt8dWhyORQKBgCTYcCS3M3WeRjuO6J3dxS+bbiuPLHYt0I3+/Fa5+GEtQnO7/nUm" +
                        "tfilnWLr9+iJ7pQs2ani4IYQ2j+L+JtjzDItD8qMFRDLsEeT7VKzXarPcpEly1NA" +
                        "DTfKGdlcv9SzS3MIwqv6hw8zHLV49hN9w19l6epXVB9aDan5Zss8kbtZAoGAb6wz" +
                        "gf6lLMxgOHawIfiQwWFLfNSeh1KOGi4qAiZVFqvbDh5OQJzCRgTo0DU1ZNdXFSvd" +
                        "jzQucC+KiLac8c25uOWNOPS+rG7tunYB64s55a+lsBv3z1ADTS2SEY09ufrXvHiF" +
                        "rt770/t4ZZ2RDELq/OKyf428Yr8SukzLUhyZgBECgYEAj34ovtfRCXbVz4BRSHB2" +
                        "og8M1BGW916lCz4PTMEKV0Fr1VxKENp1KuioQvpk2RCcpT7PIyiBXuHex20wjKBI" +
                        "coB1a1+66C6oMODFT/uOaLC7/qzrW0HGRK9nn6qIVR6sE+g7kiVZ67raHD0ruGaq" +
                        "5Z2VQ43sioI1VT9/YTzr190=\n" +
                        "-----END RSA PRIVATE KEY-----";
            }
            else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Formal")
            {
                return "-----BEGIN PRIVATE KEY-----\n" +
                        "MIICeAIBADANBgkqhkiG9w0BAQEFAASCAmIwggJeAgEAAoGBAKB7PWmbAEIZH+xW" +
                        "bI1BzQ1DZayAC5VrDDD6vTuP91EqzkyuKsP7F2QSom97M5cqvlAsJTb1QxvpFkeu" +
                        "UZJAZcPAOpjugEAsyuf2XIV/Jdc5edVU23/GTy2iNaEC4VRZm9mu9qOAW46bhBuI" +
                        "B2g4CdjOOJucFMgjifAf7oIZb6SHAgMBAAECgYATmhoNPS6R5Em+72uA8YmfVh0k" +
                        "Ldu8W3/4t5fHn7Ugx+689QUqPRBFW/VVUqIhk8n9NvcwuFKDJTxgcw1ppM4JIms1" +
                        "ob5EIBNf+IgcQZE8Jdyb/5zjVYctK8ZpnzJEENYwhQo3aqnH23SjchgmoxaLsrX9" +
                        "8Iq/rHYo3wbIjwNTgQJBANWhcg+pzE/NNRqNQdUZs4qbkpbYrIR3T7SErgeIR6NR" +
                        "8z5EVk/jtqoMrNOaodm/KptxlDWKYvEjcpGu0dFHhMcCQQDAT0bBjBpvqtZDdEm4" +
                        "Acl24QP1Vw/xJC5Luct/v6pGOPipV26EL3zz2WPvof+Ke3TyJ+Ak3Ra2EUDJpY7m" +
                        "t6JBAkEAs8aEBRhgHjvTV3/OCbXyOE+JQbYfH+6Dvab2CNKvboo1R44ckGhRvLKj" +
                        "8gKr92D/SwZ+sbu+Xaz16hES8qnb2wJBAJrj/zhKkMxaC8M+uVV0UwDl0VtcqSFv" +
                        "I76oQah+BjYTEuzJ1+CtovWApYZPs1Olhha4WUc3r2ArW0ROjV90jkECQQCKYFtM" +
                        "CJoCtvLhvIO2TZrmFGPnDB+WiA904/j15CE4oAa3W7G9DWbhLJg8Uh33Qi1VYIVK" +
                        "MD5TgYLtxEgz9fS/\n" +
                        "-----END PRIVATE KEY-----";
            }
            return null;
        })();

        #endregion

    }
}

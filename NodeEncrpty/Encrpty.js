var crypto = require('crypto');
var fs = require('fs');

module.exports = {
    //MD5加密 参数content内容
    Md5encrypt: function(callback, content) {
        var md5 = crypto.createHash('md5');
        md5.update(content);
        var result = md5.digest('hex').toUpperCase();
        callback(null, result);
        //return result;
    },
    //DES加密 加密内容 和 加密key8位
    DESencrypt: function(callback, plaintext, key) {
        var ecb = 'DES';
        var enkey = new Buffer(key);
        var iv = key;
        var eniv = new Buffer(iv ? iv : 0, 'binary');
        var cipher = crypto.createCipheriv(ecb, enkey, eniv);
        cipher.setAutoPadding(true) //default true
        var ciph = cipher.update(plaintext, 'utf8', 'base64');
        ciph += cipher.final('base64');
        callback(null, ciph);
        //return ciph;
    },
    //DES解密 解密内容 和 解密key8位
    DESdecrypt: function(callback, encrypt_text, key) {
        var ecb = 'DES';
        var dekey = new Buffer(key);
        var iv = key;
        var deiv = new Buffer(iv ? iv : 0, 'binary');
        var decipher = crypto.createDecipheriv(ecb, dekey, deiv);
        decipher.setAutoPadding(true);
        var txt = decipher.update(encrypt_text, 'base64', 'utf8');
        txt += decipher.final('utf8');
        callback(null, txt);
        //return txt;
    },
    // RSA加密 加密内容和证书串
    RSAencrypt: function(callback, plaintext, key) {
        var data = new Buffer(plaintext);
        var result = crypto.publicEncrypt({ key: key, padding: crypto.constants.RSA_PKCS1_PADDING }, data).toString('base64');
        callback(null, result);
        //return result;
    },
    // RSA加密 加密内容和证书CER格式
    RSAencryptCER: function(callback, plaintext, keypath) {
        var pub_key = fs.readFileSync(keypath);
        var pkey = pub_key.toString('ascii');
        var data = new Buffer(plaintext);
        var result = crypto.publicEncrypt({ key: pkey, padding: crypto.constants.RSA_PKCS1_PADDING }, data).toString('base64');
        callback(null, result);
        //return result;
    },
    // RSA解密 加密内容和私钥证书串
    RSAdecrypt: function(callback, encrypt_text, key) {
        var data = new Buffer(encrypt_text, 'base64');
        var result = crypto.privateDecrypt({ key: key, passphrase: '123456', padding: crypto.constants.RSA_PKCS1_PADDING }, data).toString('utf8');
        callback(null, result);
        //return result;
    },
    // RSA解密 加密内容和私钥证书CER格式
    RSAdecryptPFX: function(callback, encrypt_text, keypath) {
        var priv_key = fs.readFileSync(keypath);
        var pkey = priv_key.toString('ascii');
        var data = new Buffer(encrypt_text, 'base64');
        var result = crypto.privateDecrypt({ key: pkey, passphrase: '123456', padding: crypto.constants.RSA_PKCS1_PADDING }, data).toString('utf8');
        callback(null, result);
        //return result;
    },
    //AES加密
    AESencrypt: function(callback, plaintext, key) {
        var ecb = 'aes-128-ecb';
        var clearEncoding = 'utf8';
        var iv = "";
        var cipherEncoding = 'base64';
        var cipher = crypto.createCipheriv(ecb, key, iv);
        var cipherChunks = [];
        cipherChunks.push(cipher.update(plaintext, clearEncoding, cipherEncoding));
        cipherChunks.push(cipher.final(cipherEncoding));
        var result = cipherChunks.join('');
        callback(null, result);
        //return result;

    },
    //AES解密
    AESdecrypt: function(callback, encrypt_text, key) {
        iv = "";
        var clearEncoding = 'utf8';
        var cipherEncoding = 'base64';
        var cipherChunks = [];
        var decipher = crypto.createDecipheriv('aes-128-ecb', key, iv);
        decipher.setAutoPadding(true);
        cipherChunks.push(decipher.update(encrypt_text, cipherEncoding, clearEncoding));
        cipherChunks.push(decipher.final(clearEncoding));
        var result = cipherChunks.join('');
        callback(null, result);
        //return result;
    }
}

//var x = 'password'
//var result = module.exports.Md5encrypt(x);
//var des_core = module.exports.DESencrypt(null, "uuiioo", "12345678");
//var des_result = module.exports.DESdecrypt(null, des_core, "12345678");

// var publickey =
//     '-----BEGIN PUBLIC KEY-----\n' +
//     'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlnix2KuaKMyFOj3ZGPYN' +
//     'pRdPEarj6u8zUvy0RPATyPv346+x+9RoqHXH9uBJTsbQ2cBmYi0oEkHsqLTebff6' +
//     'PAvTfHiyBKJe1khmWtbwVJRXfRN/hK3jbobREm/GXoJKV7VZCVL3fkND7Pfhkc9W' +
//     'khH7h0//4dH3vB0ww8o5jGtMn2Rh92eGylyoBd8KcS0+j7mywHe17KS+++MTCkU3' +
//     'Hnh4Xx2We0v3RSb/lC8/SdIe++3TsbPeQ5ICTM2K01oGiqqZGZqD5xLhXWQW5e8I' +
//     'b6oems/iY2t/B6CosDkeHo0b6XBGis7J/ggjz0SJDA1/NbCF/cIANLlk6/Qwqk1S' +
//     'VwIDAQAB\n' +
//     '-----END PUBLIC KEY-----';

// var publickey =
//     "-----BEGIN CERTIFICATE-----\n" +
//     "MIIDMjCCAh6gAwIBAgIQrFnANiqIyp9CujnxnDoMfTAJBgUrDgMCHQUAMCkxJzAl" +
//     "BgNVBAMeHgBpAG4AdABmAG8AbwAxADEAXwAzADkAOQBfADMANDAeFw0xNjA4MTgx" +
//     "MzEzMzRaFw0zOTEyMzEyMzU5NTlaMCkxJzAlBgNVBAMeHgBpAG4AdABmAG8AbwAx" +
//     "ADEAXwAzADkAOQBfADMANDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEB" +
//     "ALzpAon/8FVGWtVvP67XgqRJeacQ2TdxC7TEiSSIfoemVI7PSWeEc+YDejOR9dT0" +
//     "bpHHiM7g3sxieQ00CT6/k8/jgS0vG+choJ0RuTE7ltXP6GQ1d9LFNbVROVv7oqQc" +
//     "ZBScwJz+p7WRQtMPJgLFww/z8iu2X6f3QT0m0vNeVmXaTtk6uBISUm/jtl/p3QZs" +
//     "hNg+lKhCy3p0L5vHv6f6TxuoPS0RUchZ+v87FiF795FknbezqE92RTniAlP/fJ4Y" +
//     "Lh3VjshmawmEi0724YWp8r5JOIfaMJx6BxcwzOjh9S7Wi58wFOMutKw4Rk1QKpre" +
//     "o+fHbidvAGQANLDu65d56D0CAwEAAaNeMFwwWgYDVR0BBFMwUYAQC1fPqsgiwOvS" +
//     "PtF8X3J14qErMCkxJzAlBgNVBAMeHgBpAG4AdABmAG8AbwAxADEAXwAzADkAOQBf" +
//     "ADMANIIQrFnANiqIyp9CujnxnDoMfTAJBgUrDgMCHQUAA4IBAQA2QPJ6nRfrWj0/" +
//     "DPmWH5T+VvtUQ3i3hXvnGBrNzU8SLeMWAGL16pQHOFnZF2mCeM4jIAtxARiN08zZ" +
//     "AW+JqGKhNBvw1IXvISxDtreNZyonI886pDGwyG2Z2miT2IAZlRfv6+AHd6Y5VMhF" +
//     "NAN/MuGFZ+W4CmFqr9baWzW4gfmxylaT49szAYOiGD3LEiX4KnEDSgIZ0DQIIoC7" +
//     "CTclC55pdMtLbd3cl8/H2oNnjoGyAnKRN1jgvKPxGMje5D5hmCMfPwgn7G0naah/" +
//     "MA8iF9YdZBA1/sGKyQQjv29H41LePu3fdRNtkyIqIc7deDxIrYP7Xr4r8mrMpfLc" +
//     "w/TSdto9\n" +
//     "-----END CERTIFICATE-----";

// var privatekey =
//     '-----BEGIN RSA PRIVATE KEY-----\n' +
//     'MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCWeLHYq5oozIU6' +
//     'PdkY9g2lF08RquPq7zNS/LRE8BPI+/fjr7H71Giodcf24ElOxtDZwGZiLSgSQeyo' +
//     'tN5t9/o8C9N8eLIEol7WSGZa1vBUlFd9E3+EreNuhtESb8ZegkpXtVkJUvd+Q0Ps' +
//     '9+GRz1aSEfuHT//h0fe8HTDDyjmMa0yfZGH3Z4bKXKgF3wpxLT6PubLAd7XspL77' +
//     '4xMKRTceeHhfHZZ7S/dFJv+ULz9J0h777dOxs95DkgJMzYrTWgaKqpkZmoPnEuFd' +
//     'ZBbl7whvqh6az+Jja38HoKiwOR4ejRvpcEaKzsn+CCPPRIkMDX81sIX9wgA0uWTr' +
//     '9DCqTVJXAgMBAAECggEAHjCXFi7P3lGyhU31aeGsOSxlC9lqoy9c79CN5qr4fUG4' +
//     'qbENl8Y1PV9sQGcdrjWLn7TPlfETch5R+kNXwV6YkUWaKAYbSXy+ZGXgS2eGjqG4' +
//     'r/qigh9VW06nUfilJz8i5VgYRhukVmvui/PsCRI9f08mVS7C2y8Ccna02wOO6lIq' +
//     'CQxfeORoKRmnusFqUZfFaCcPm57jXACx51jGa3ejsbeLCtnxxaS+c3sXwzQUwOg5' +
//     'i/G6gvTyDUiVdfNwZwmhZW8m2M4UX3H6/v87cH0OuqJnocYfCtHYF+8gB7ocDSmv' +
//     'bJYQIOiGZp6N6FtnU/twG/JRmb6ancGStP47WRH7gQKBgQC5RAa+mQESfGUkwHWM' +
//     'gaInAm5G1RIVsyXM21j2eQLrrykuZtwm9Mr+b4u+q0BfGwAy6yE7F1AsB89XM4h7' +
//     'B7ph7RZ4+oxQ30Nc3Gy1+a8AklwXoDzDnMngnZGk2rQEeUTXum5ZFGd4+Q+iGbOy' +
//     'Sty8ge9Tc6tGQtSlta36vRfOlwKBgQDP69uh50VRGC1ZK7q8PKOzdmPIOqB2Skb5' +
//     'Snt5rwaySI+8jrumoZNKZ+McB4+nI3NQCVZP5FMMx7E/mVv9T1TzbOodg2ak7zHZ' +
//     'BRRhEIftFLYfqYeFKlXJwchNveurBD0PZb7NsHyhEuRdhI5LaAa5kEsCSykUKHFE' +
//     'LOQtQcbSQQKBgQCmEjelhMv0QTP9RNRaVh3/r+sgbYEfmI5K21kc347hrAtTP/sa' +
//     'pJaOi3kOOr6iQjbkbedE6/n+7iVLerNd7aiCfZ+GZLmBCRF+XUzJtv7LmWcZ+pZd' +
//     'lt9UDIrw5uc1l2tBoimmimbShxgfa8CsEvVZuXPKL8aZXfD2dix1tilwEwKBgCCO' +
//     'mAeUBSCbno418LmE7UUuppU1yOZ83vwuE4OMSookHo2x8+Q1rLCb7831ySKsxGOl' +
//     '8qPz5qU7p5+DzgmuIGk1hrs0ViBBbBbFWtmQEximg5YVn6jUZjZ/Z3P05zASK9V7' +
//     'YxMmLX2zTZrvJLDeCx+HXpdvWXFUm9fHB7umbxGBAoGABGoSjBndUbSawQZqANjo' +
//     'JbiM4cefvhwnqB73mF6P3aTGDJv0oSFLk+WjpcLJi2vsRcyCHZfEoMQxQVehCCN3' +
//     '9XzX4bJKFcGsX1AVDROyM0phlXJbeGUlGDWje8iIY0AzZuBpiSHe5M5emUTN2XVS' +
//     'Cn4HwiBrTl6mqOKw4Dz1vFM=\n' +
//     '-----END RSA PRIVATE KEY-----';

// var privatekey =
//     "-----BEGIN PRIVATE KEY-----\n" +
//     "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC86QKJ//BVRlrV" +
//     "bz+u14KkSXmnENk3cQu0xIkkiH6HplSOz0lnhHPmA3ozkfXU9G6Rx4jO4N7MYnkN" +
//     "NAk+v5PP44EtLxvnIaCdEbkxO5bVz+hkNXfSxTW1UTlb+6KkHGQUnMCc/qe1kULT" +
//     "DyYCxcMP8/Irtl+n90E9JtLzXlZl2k7ZOrgSElJv47Zf6d0GbITYPpSoQst6dC+b" +
//     "x7+n+k8bqD0tEVHIWfr/OxYhe/eRZJ23s6hPdkU54gJT/3yeGC4d1Y7IZmsJhItO" +
//     "9uGFqfK+STiH2jCcegcXMMzo4fUu1oufMBTjLrSsOEZNUCqa3qPnx24nbwBkADSw" +
//     "7uuXeeg9AgMBAAECggEAbrVDJqqWJvNnMiPTN1zXE/53S8Ak/EfEq3huQrm3JToF" +
//     "3fd+tiq6XTb2e5hzcAyba9V6yHqiNzcSZ/lx3ZMaRb+3KUqtWIOtg78qP78DQ2Rn" +
//     "daqcASpPsmXabpyGkRSOVkIS4y6eDFsHxXbgOMm66fN1ncZwQpg1ZGgHQlar3V3T" +
//     "F4MPebYFUQJlN3qcNRNrt9NV4F99iSuEKfjNyxQ2TdbKyxuj6gooj7xKEUZNfmfX" +
//     "FocZrrDlxI+NHNs+BWTsTZ4GfGd9wNjkqvrX2RqV7Bc8H65LAcxBxbnzmdms0Fs8" +
//     "1xlYw6f3zuuwq6q25QKOXat5GZCmBo5NAtvBrgmGAQKBgQDvrpIZ1eKQasLbWDCs" +
//     "wm1U0XC+gQTZsvSxf7BcoYUgEUKEpEq2tfRos5NE5OWCqdzxYGqFBAXf9ajOszWZ" +
//     "BuMFeqyG5BYTrY2sPUDuvTUZUxusQ8WaQL5At+1LNERTQORD+jcUrJdZX+UwdijL" +
//     "XvbejuBWllPWzme/GXisFH8IvQKBgQDJxYk8vAGxWXtHEIyy4JIdkzEB0hM2fTDz" +
//     "jT6I2jnMdwRT5K7No1tE80k1j7CqsJxwgLQcPMO3opWIn4ox3Y/Wald+uH0Xj9tz" +
//     "8xYUIyZ9dSRIEMstFxqv5yrcwBCuM0sdndN39UPSdQinqjFYsLwAxmJB/7NxruU6" +
//     "bZPn4CsVgQKBgQDizC+XntpaiYuE/vhgIUJPZjfnLDKoxTvh0c/Sd0x/QZXN6aCB" +
//     "L7/sazBJmsbKU22sQe3JnDrq54wqu01WVjvv7Vrf8WgqiqlqeDO/NO7P3eJrqV2M" +
//     "TwM4PlKePQECPn9BC72fJIm6mpFGwCht+JhllJEXnfwLYRiura2QN3zViQKBgEhe" +
//     "c3S3Mhw8Lf01MGHQLlgxPPicMovupyoT1QQc0XJGleTJNvFG0CuQ3wc4/HKcWd/4" +
//     "KaGfIcuxotSX9GciQr7y8WLgJO3YiSuzWo3qdZm19EdszpLjoIzK6xK2SMVSXHTI" +
//     "8NorvCG2HF6WqpBXA3d7sC5f2irCQjGMr+oQ+yMBAoGBAI39ZhcHXmaRq6mlbSKL" +
//     "QmUy7/uqUnDt8mXmX4D7ZLTgGuumzcZY3O+2frGKibLx/SlC7mTPct0xBlPEfcNj" +
//     "DBzrXOqZZUmY8AGSdS1DMOR5WHlTvVHnoEuULT09JiU0jPX/KDiDFCB+Ult8OhiR" +
//     "Bpv4iqQFcQxK//LtYQoNHMMj\n" +
//     "-----END PRIVATE KEY-----";

//openssl pkcs12 -in intfoo30_898_33.pfx -nocerts -out intfoo30_898_33_pfx.pem -nodes
//openssl x509 -inform der -in intfoo30_898_33.cer -pubkey -out intfoo30_898_33_cer.pem
// var data = "uuiioo";
// var rsa_core = module.exports.RSAencryptCER(null, data, "C:\\certificate\\ceshizhuzhanghu\\intfoo30_898_33_cer.pem");
// var rsa_result = module.exports.RSAdecryptPEM(null, rsa_core, null);
// var rsa_core = module.exports.RSAencrypt(null, data, publickey);
// var rsa_result = module.exports.RSAdecrypt(null, rsa_core, privatekey);

// 1V8VL5WwK7bZBH7x0k2ATF/sx8L7Gprlif0kP8eAu7A=
// var AESdata = "Z8V671AH829WIN94";
// var AES_Key = "eatfast9abcksdcz";
// var aes_core = module.exports.AESencrypt(null, AESdata, AES_Key);
// var aes_reuslt = module.exports.AESdecrypt(null, aes_core, AES_Key);

// var rsa_core = "XNWOr67WtemHEBr1MmN6xWB8c9uJipswoh+Y+Tpz7t6i/exYrHsuqCkrQAQRnCq8i7vRD8VdT+04e2IV5cghMwKp7LGEmIcIEnxdL9oMmOBGA9Xu2XTRzjPEN4mDUrIl8IWhuGFUTXdya8Ws5PRlnjPjWmXoDAqTr1wt0ssyjoWr6z9kPt2sLki0esXvPi1ORGxgyqxfYmr3hKnInFM05+3rjgase4JisxAJ6M5ZO/tcqqIwsDCs46uDcJhjJtW56Ri6mRdGfzoatNfuemtCnq0ax4bneJiuDPE7vOJxIydf1+/5cvLRftE8r3qHzuSW5Qu9u0sJsV9mkFivKozEdA==";
// var rsa_result = module.exports.RSAdecrypt(null, rsa_core, privatekey);
/* 
    * 
    * handsontable authorization code generation
         * pKey: custom key (default is empty, if passed, will be adjusted based on this key)
         * days: number of registration days, the current time is the starting point
         * Example of use:
         * genKey();// Random generation valid for 10 days
         * genKey(null,100);//Random generation valid for 100 days
         * genKey ("11111-11111-11111-11111-11111", 100); / / modified to a valid key
    * 
    */
function test() {
    alert();
}
function HandsOnTableKey(pKey,days){
 
                // Generate a random hexadecimal code of the specified length
    var g = function(len,s){
        s = s || "";
        var rev = [];
        for(var i=s.length;i<len;i++){
            rev.push(Math.floor(Math.random()*16).toString(16))
        }
        return s+rev.join("");
    }
                // Verify the specified interval string in the Key, and generate a check code replacement
    var e = function(str,s,len,vlen){
        var j = "00";
        var j2 = parseInt(j,16).toString();
        var j3 = parseInt(str.substr(s,len), 16) + j2.padStart(2, '0');
        var j4 = parseInt(j3,10);
        var key = (97 - (j4 % 97) + 1).toString(16).padStart(2, '0');
        var rev = str.substr(0,len+s) + key + str.substr(s+len+vlen);
        return rev;
    }
 
                //Register days
    days = days || 10;
    pKey = pKey&&pKey.replace(/-/g,'');
    if(!pKey||pKey.length!==25){
        pKey=g(25);
    }
        
                // Generate days days valid
                var p = parseInt(pKey.substr(1,1),16);//time variable (random)
    var dayKey = ((p||9)*(days+Math.floor(Date.now()/8.64e7)))
    .toString(16)
                .padStart(5,0);//days*p get the key
                var pKey = pKey.substr(0,18) + dayKey + pKey.substr(23);//Replace Key
 
    //'\x42\x3C\x48\x34\x50\x2B';//"B<H4P+"; [1,-5,7,-13,15,-22]
    var i = [1,-5,7,-13,15,-22];
 
                pKey = e(pKey,0,6,2);//Generate a 0-5 digit checksum and replace 6-7 digits
                pKey = e(pKey,6,8,2);//Generate a 6-14 bit checksum and replace 14-15 bits
                pKey = e(pKey,14,9,2);//Generate 14-23 bit checksum and replace 23-24 bits
 
    return [
        pKey.substr(0,5),
        pKey.substr(5,5),
        pKey.substr(10,5),
        pKey.substr(15,5),
        pKey.substr(20,5)
    ].join("-");
}
 
/*
11111-14f11-11111-91104-66555
2085e-82a70-f5421-12028-60832
80e19-82567-8c6f5-cd828-6082f
09c0f-41fb8-0d244-dfe28-60862
f09f0-906a1-e6e91-57828-60810
730e9-34076-bef03-cae0d-75829
3fb0c-53840-b2be4-eae43-4b824
b7f7f-14244-ebf33-df51f-67829
cb2ae-d10b2-270b3-74b31-59842
bc6d0-4138a-ab115-08835-d6027
42cfc-922b1-59f84-e3308-f902f
a1506-25148-f52e2-d3f04-7c819
c4634-223de-1c920-69611-f2050
e9c61-e51fa-2fad2-6fd28-60857
82375-04f00-ae673-cf208-f900d
41579-36209-f3362-a6d04-7c817
d78ab-f54d0-c04b4-50e1f-67818
e1fd4-31f3d-f1493-11104-7c84b
c8b66-b26f6-81c53-d4023-e4042
0252a-1028e-84be1-22d08-f9058
c2e07-25d7c-770a2-c2408-f902b
e2b72-a2a30-5d774-8af08-f9031
772f4-71280-483e0-a7c1f-6783e
b5b1d-23e84-3ebf0-a1116-6e80f
292ed-160ed-578e3-e9928-60820
1bff4-65bf9-f6f93-fbb31-59853
18e74-53f68-ba8c4-a7d23-e404d
f3e9c-24ca9-0be91-6cf0d-75830
*/
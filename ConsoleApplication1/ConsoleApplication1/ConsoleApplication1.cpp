// ConsoleApplication1.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"

class Test {
	int x;
public:
	Test() {
		x = 0;
		printf("コンストラクタ起動【test】\n");
	}

	void SetX(void) {
		x = 8;
		printf("xに%dを代入しました\n", x);
	}
};
class Test2 : public Test {
	int x;
public:
	Test2() {
		x = 0;
		printf("コンストラクタ起動【test2】\n");
	}
	void SetX(int temp = 8) {
		x = temp;
		printf("xに%dを代入しました\n", x);
	}
};
int main(void) {
	Test2 test;
	test.SetX();
	test.SetX(5);
	test.Test::SetX();
	return(0);
}


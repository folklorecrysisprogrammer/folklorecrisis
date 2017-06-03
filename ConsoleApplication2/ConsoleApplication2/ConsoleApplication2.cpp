// ConsoleApplication2.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"
#include <time.h>

volatile int a;
void Method1()
{
	for (int i = 0; i < 4; i++)
	{
		for (int j = 0; j < 1024 * 1024; j++)
		{
			a = 0;
		}
	}
}

void Method2()
{
	for (int i = 0; i < 1024 * 1024; i++)
	{
		for (int j = 0; j < 4; j++)
		{
			a = 0;
		}
	}
}

int main()
{
	Time::Begin();
	for (int i = 0; i < 10000; i++) {
		Method1();
	}
	Time::End();
	double d = Time::GetTime();
	printf("%f", &d);


	// Method2();

    return 0;
}


static class Time {
	static clock_t beforeTime;
	static double deltaTime;
public:
	static void Begin() {
		beforeTime = clock();
	};
	static void End() {
		static int cont = -1;
		cont++;
		deltaTime = (double)((clock() - beforeTime) / 1000.0);
	};
	static double GetTime() { return deltaTime; };
};


// ConsoleApplication1.cpp : �R���\�[�� �A�v���P�[�V�����̃G���g�� �|�C���g���`���܂��B
//

#include "stdafx.h"

class Test {
	int x;
public:
	Test() {
		x = 0;
		printf("�R���X�g���N�^�N���ytest�z\n");
	}

	void SetX(void) {
		x = 8;
		printf("x��%d�������܂���\n", x);
	}
};
class Test2 : public Test {
	int x;
public:
	Test2() {
		x = 0;
		printf("�R���X�g���N�^�N���ytest2�z\n");
	}
	void SetX(int temp = 8) {
		x = temp;
		printf("x��%d�������܂���\n", x);
	}
};
int main(void) {
	Test2 test;
	test.SetX();
	test.SetX(5);
	test.Test::SetX();
	return(0);
}


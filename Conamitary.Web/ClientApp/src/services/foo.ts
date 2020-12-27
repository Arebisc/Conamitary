import { FooInterface } from '@/abstract/fooInterface';
import { injectable } from 'inversify';

@injectable()
export class Foo implements FooInterface {
    public bar(): void {
        console.log('bar called');
    }
}

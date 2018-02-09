using System.IO;
using System;
using System.Text;
using System.Net;

public class ByteStream
{
    public static readonly int Bool_Size = sizeof(byte);
    public static readonly int Byte_Size = sizeof(byte);
    public static readonly int Short_Size = sizeof(short);
    public static readonly int UShort_Size = sizeof(ushort);
    public static readonly int Int_Size = sizeof(int);
    public static readonly int UInt_Size = sizeof(uint);
    public static readonly int Long_Size = sizeof(long);
    public static readonly int ULong_Size = sizeof(ulong);
    public static readonly int Float_Size = sizeof(float);
    public static readonly int Double_Size = sizeof(double);
    /* 
     * 主机一般是低位有效字节在前：小端模式
     * 网络一般是高位有效字节在前：大端模式
     * 网络字节读写需要逆转字节顺序
     */
    bool _isBigEndian = true;
    private byte[] _bytes;
    int _position;
    int _length;
    public ByteStream(int len)
    {
        if(len > 0)
            Reset(new byte[len]);
    }
    public ByteStream(byte[] bytes = null)
    {
        Reset(bytes);
    }
    public void Reset(byte[] bytes = null)
    {
        _position = 0;
        _bytes = bytes;
        _length = (null == _bytes) ? 0 : _bytes.Length;
    }
    public bool isBigEndian
    {
        get { return _isBigEndian; }
        set { isBigEndian = value; }
    }
    private bool needReverseBytes { get { return _isBigEndian; } }
    public int position
    {
        get
        {
            return _position;
        }
        set
        {
            if (value < 0)
            {
                _position = 0;
            }
            else if (value > length)
            {
                _position = length;
            }
            else
            {
                _position = value;
            }
        }
    }
    public int capacity { get { return null == _bytes ? 0 : _bytes.Length; } }
    public int length
    {
        get
        {
            return (null == _bytes) ? 0 : _length;
        }
        set
        {
            if (null != _bytes)
            {
                _length = Math.Min(_bytes.Length, value);
            }
        }
    }
    public int bytesAvailable
    {
        get
        {
            if (null == _bytes) return 0;
            if (_position >= _length) return 0;
            return _length - _position;
        }
    }
    public byte[] buffer
    {
        get
        {
            return _bytes;
        }
    }
    public byte this[int i]
    {
        get
        {
            return _bytes[i];
        }
        set
        {
            _bytes[i] = value;
        }
    }
    public void Clear()
    {
        Clear(false);
    }
    public void Clear(bool clearBuffer)
    {
        _position = 0;
        _length = 0;
        if (clearBuffer)
            _bytes = null;
    }

    public byte ReadByte()
    {
        if (null == _bytes || _position >= _length || (_position + Byte_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        int result = _bytes[_position];
        _position += Byte_Size;
        return (byte)result;
    }
    public bool ReadBool()
    {
        if (null == _bytes || _position >= _length || (_position + Bool_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        int result = _bytes[_position];
        _position += Bool_Size;
        return 0 != result;
    }
    public short ReadShort()
    {
        if (null == _bytes || _position >= _length || (_position + Short_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        short result = BitConverter.ToInt16(_bytes, _position);
        if (this.needReverseBytes)
        {
            result = IPAddress.NetworkToHostOrder(result);
        }
        this._position += Short_Size;
        return result;
    }
    public ushort ReadUShort()
    {
        if (null == _bytes || _position >= _length || (_position + UShort_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        ushort result = BitConverter.ToUInt16(_bytes, _position);
        if (this.needReverseBytes)
        {
            result = (ushort)IPAddress.NetworkToHostOrder((short)result);
        }
        this._position += UShort_Size;
        return result;
    }
    public int ReadInt()
    {
        if (null == _bytes || _position >= _length || (_position + Int_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        int result = BitConverter.ToInt32(_bytes, _position);
        if (this.needReverseBytes)
        {
            result = IPAddress.NetworkToHostOrder(result);
        }
        this._position += Int_Size;
        return result;
    }
    public uint ReadUInt()
    {
        if (null == _bytes || _position >= _length || (_position + UInt_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        uint result = BitConverter.ToUInt32(_bytes, _position);
        if (this.needReverseBytes)
        {
            result = (uint)IPAddress.NetworkToHostOrder((int)result);
        }
        this._position += UInt_Size;
        return result;
    }
    public long ReadLong()
    {
        if (null == _bytes || _position >= _length || (_position + Long_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        long result = BitConverter.ToInt64(_bytes, _position);
        if (this.needReverseBytes)
        {
            result = IPAddress.NetworkToHostOrder(result);
        }
        this._position += Long_Size;
        return result;
    }
    public ulong ReadULong()
    {
        if (null == _bytes || _position >= _length || (_position + ULong_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        ulong result = BitConverter.ToUInt64(_bytes, _position);
        if (this.needReverseBytes)
        {
            result = (ulong)IPAddress.NetworkToHostOrder((long)result);
        }
        this._position += ULong_Size;
        return result;
    }
    public float ReadFloat()
    {
        if (null == _bytes || _position >= _length || (_position + Float_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        float result = BitConverter.ToSingle(_bytes, _position);
        if (this.needReverseBytes)
        {
            byte[] bytes = BitConverter.GetBytes(result);
            Array.Reverse(bytes);
            result = BitConverter.ToSingle(bytes, 0);
        }
        this._position += Float_Size;
        return result;
    }
    public double ReadDouble()
    {
        if (null == _bytes || _position >= _length || (_position + Double_Size > _length))
        {
            throw new IndexOutOfRangeException();
        }
        double result = BitConverter.ToDouble(_bytes, _position);
        if (this.needReverseBytes)
        {
            byte[] bytes = BitConverter.GetBytes(result);
            Array.Reverse(bytes);
            result = BitConverter.ToDouble(bytes, 0);
        }
        this._position += Double_Size;
        return result;
    }
    public string ReadString()
    {
        if (null == _bytes || _position >= _length)
        {
            throw new IndexOutOfRangeException();
        }
        ushort len = this.ReadUShort();
        string result = Encoding.UTF8.GetString(_bytes, _position, len);
        this._position += len;
        return result;
    }

    public void Read(byte[] bytes, int offset = 0, int length = 0)
    {
        if (null == _bytes || position >= _length)
        {
            throw new IndexOutOfRangeException();
        }
        int num = length > 0 ? length : (_length - _position);
        Array.Copy(_bytes, _position, bytes, offset, num);
        _position += num;
    }

    ///**
    // *****************************  FUCK YOU ALL ********************************************************
    // */
    public void WriteByte(byte value)
    {
        int newPostion = _position + 1;
        CheckAlloc(newPostion);
        _bytes[_position] = value;
        _position = newPostion;
        if (_position > _length)
            _length = _position;
    }
    public void WriteBool(bool value)
    {
        byte tmp = (byte)(value ? 1 : 0);
        this.WriteByte(tmp);
    }


    public void WriteShort(short value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteInt(int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteLong(long value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteUShort(ushort value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteUInt(uint value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteULong(ulong value)
    {
        byte[] bytes = BitConverter.GetBytes(value);

        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteFloat(float value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteDouble(double value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (this.needReverseBytes)
        {
            Array.Reverse(bytes);
        }
        this.Write(bytes, 0, bytes.Length);
    }
    public void WriteString(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        this.WriteShort((short)bytes.Length);
        this.Write(bytes);
    }
    public void Write(ByteStream bytes, int offset = 0, int length = 0)
    {
        byte[] byteArr = bytes.buffer;
        this.Write(byteArr, offset, length);
    }
    public void Write(byte[] bytes, int offset = 0, int length = 0)
    {
        int numBytes = (length > 0) ? length : (bytes.Length - offset);
        
        int newPosition = _position + numBytes;
       
        CheckAlloc(newPosition);
        
        Array.Copy(bytes, offset, _bytes, _position, numBytes);
        
        _position = newPosition;

        if (_position > _length)
            _length = _position;
    }
    void CheckAlloc(int size)
    {
        if (null == _bytes || _bytes.Length < size)
        {
            byte[] newBytes = new byte[2 * size];
            if (null != _bytes)
            {
                Array.Copy(_bytes, 0, newBytes, 0, _length);
            }
            _bytes = newBytes;
        }
    }
}//end class
